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
        public async Task<ActionResult> Index(int pageNumber = 1, int authorId = 0)
        {
            return View((await db.Entities
                            .Include(col => col.Authors)
                            .Where(c => authorId == 0 || c.Authors.Any(a => a.Id == authorId))
                            .Where(col => col.IsVisible)
                            .ToListAsync())
                            .Select(col => new TranslatedViewModel<Collection, CollectionTranslation>(col))
                            .ToPagedList(pageNumber, 10));
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

            collection.Authors = collection.Authors.ToList();
            collection.Documents = collection.Documents.ToList();

            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(new CollectionDetailsViewModel
                {
                    Collection = new TranslatedViewModel<Collection, CollectionTranslation>(collection),
                    Documents = collection.Documents.Select(d => new TranslatedViewModel<Document, DocumentTranslation>(d)).ToList(),
                    Authors = collection.Authors.Select(a => new TranslatedViewModel<Author, AuthorTranslation>(a)).ToList()
                });
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

