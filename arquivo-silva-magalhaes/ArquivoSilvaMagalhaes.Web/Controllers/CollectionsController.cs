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
    public class CollectionsController : Controller
    {
        /// <summary>
        /// Associa Entidade Collections às traduções existentes
        /// </summary>
        private ITranslateableRepository<Collection, CollectionTranslation> db;

        public CollectionsController()
            : this(new TranslateableRepository<Collection, CollectionTranslation>()) { }

        public CollectionsController(ITranslateableRepository<Collection, CollectionTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece lista de coleções paginada e ordenada por data de fim de produção por ordem descendente
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int pageNumber = 1, int authorId = 0, string query = "")
        {
            var model = await db.Entities
                .Where(c => authorId == 0 || c.Authors.Any(a => a.Id == authorId))
                .Where(c => query == "" || c.Translations.Any(t => t.Title.Contains(query)))
                .Where(col => col.IsVisible)
                .OrderBy(col => col.Id)
                .Select(col => new TranslatedViewModel<Collection, CollectionTranslation>
                {
                    Entity = col
                })
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.Query = query;
            ViewBag.AuthorId = authorId;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CollectionList", model);
            }

            return View(model);
        }

        /// <summary>
        /// Fornece detalhes de uma determinda coleção
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.GetByIdAsync(id);

            if (collection == null || !collection.IsVisible)
            {
                return HttpNotFound();
            }

            collection.Authors = collection.Authors.ToList();
            collection.Documents = collection.Documents.ToList();

            return View(new TranslatedViewModel<Collection, CollectionTranslation>(collection));
        }

        /// <summary>
        /// Faz corresponder o documento à sua coleção 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Docs(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.GetByIdAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<Collection, CollectionTranslation>(collection));
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

