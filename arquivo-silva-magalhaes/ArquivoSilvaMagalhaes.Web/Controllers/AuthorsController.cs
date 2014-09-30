using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class AuthorsController : Controller
    {

        /// <summary>
        /// Associa Entidade Author às traduções existentes
        /// </summary>
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
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                            .ToListAsync())
                            .Select(a => new TranslatedViewModel<Author, AuthorTranslation>(a))
                            .ToPagedList(pageNumber, 10));
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
            return View(new AuthorDetailsViewModel
                    {
                        Author = new TranslatedViewModel<Author, AuthorTranslation>(author),
                        Collections = author.Collections.ToList().Select(c => new TranslatedViewModel<Collection, CollectionTranslation>(c)),
                        Documents = author.Documents.ToList().Select(d => new TranslatedViewModel<Document, DocumentTranslation>(d))
                    });
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
