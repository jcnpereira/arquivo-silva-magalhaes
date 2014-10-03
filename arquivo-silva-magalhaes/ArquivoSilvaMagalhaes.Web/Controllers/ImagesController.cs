using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ImagesController : Controller
    {
        /// <summary>
        /// Associa Entidade Author às traduções existentes
        /// </summary>
        private ITranslateableRepository<Image, ImageTranslation> db;

        public ImagesController()
            : this(new TranslateableRepository<Image, ImageTranslation>()) { }

        public ImagesController(ITranslateableRepository<Image, ImageTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece lista paginada de imagens correspondentes ao documento onde estão inseridas 
        /// e ordenada pelo id da imagem
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(
            int collectionId = 0,
            int documentId = 0,
            int keywordId = 0,
            int classificationId = 0,
            bool hideWithoutImage = false,
            string query = "",
            int pageNumber = 1)
        {
            var model = (await db.Entities
                .Include(i => i.Document.Collection)
                .Include(i => i.Document)
                .Include(i => i.Translations)
                .Where(i =>
                    (i.IsVisible && i.Document.Collection.IsVisible) &&
                    (!hideWithoutImage || i.ImageUrl != null) &&
                    (collectionId == 0 || i.Document.CollectionId == collectionId) &&
                    (documentId == 0 || i.DocumentId == documentId) &&
                    (keywordId == 0 || i.Keywords.Any(k => k.Id == keywordId)) &&
                    (classificationId == 0 || i.ClassificationId == classificationId))
                .Where(i => query == "" || i.Translations.Any(t => t.Title.Contains(query)))
                .OrderBy(i => i.Id)
                .ToListAsync())
                .Select(img => new TranslatedViewModel<Image, ImageTranslation>(img))
                .ToPagedList(pageNumber, 12);

            ViewBag.Query = query;
            ViewBag.CollectionId = collectionId;
            ViewBag.DocumentId = documentId;
            ViewBag.ClassificationId = classificationId;
            ViewBag.KeywordId = keywordId;


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ImageList", model);
            }

            return View(model);
        }

        /// <summary>
        /// Forence os detalhes de determinada imagem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.GetByIdAsync(id);
            if (image == null || !image.IsVisible || !image.Document.Collection.IsVisible)
            {
                return HttpNotFound();
            }

            var specimens = image.Specimens.ToList();
            var formats = specimens.Select(s => s.Format).ToList();
            var processes = specimens.Select(s => s.Process).ToList();

            return View(new ImageDetailsViewModel
                {
                    Image = new TranslatedViewModel<Image, ImageTranslation>(image),
                    Classification = new TranslatedViewModel<Classification, ClassificationTranslation>(image.Classification),
                    Keywords = image.Keywords.ToList().Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k)),
                    Specimens = specimens.Select(s => new TranslatedViewModel<Specimen, SpecimenTranslation>(s)),
                    Processes = processes.Select(p => new TranslatedViewModel<Process, ProcessTranslation>(p)),
                    Formats = formats
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
