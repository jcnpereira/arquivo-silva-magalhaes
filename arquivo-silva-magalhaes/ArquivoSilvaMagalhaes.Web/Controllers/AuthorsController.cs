using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Common;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class AuthorsController : Controller
    {
        private ITranslateableRepository<Author, AuthorTranslation> db;

        public AuthorsController()
            : this(new TranslateableRepository<Author, AuthorTranslation>()) { }

        public AuthorsController(ITranslateableRepository<Author, AuthorTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece lista de autores paginada e ordenada por data de nascimento por ordem descendente
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int pageNumber = 1, string query = "")
        {
            var model = await db.Entities
                .Where(a => query == "" || a.LastName.Contains(query) || a.FirstName.Contains(query))
                .Include(a => a.Translations)
                .OrderBy(a => a.Id)
                .Select(a => new TranslatedViewModel<Author, AuthorTranslation>
                {
                    Entity = a
                })
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.Query = query;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_AuthorList", model);
            }

            return View(model);
        }

        /// <summary>
        /// Forece detalhes de um determindo autor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.GetByIdAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<Author, AuthorTranslation>(author));
        }

        /// <summary>
        /// Actualização à base de dados
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
