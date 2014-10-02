using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
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
            int documentId = 0,
            int keywordId = 0,
            int classificationId = 0,
            bool hideWithoutImage = false,
            string query = "",
            int pageNumber = 1)
        {
            var model = (await GetImages(documentId, keywordId, classificationId, hideWithoutImage, query))
                .Select(img => new TranslatedViewModel<Image, ImageTranslation>(img))
                .ToPagedList(pageNumber, 2);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ImageList", model);
            }

            return View(model);
        }

        private async Task<IEnumerable<Image>> GetImages(
            int documentId = 0,
            int keywordId = 0,
            int classificationId = 0,
            bool hideWithoutImage = false,
            string query = "")
        {
            var lang = Thread.CurrentThread.CurrentUICulture.Name;

            return await db.Entities
                .Include(i => i.Translations)
                .Where(i =>
                    i.IsVisible &&
                    (!hideWithoutImage || i.ImageUrl != null) &&
                    (documentId == 0 || i.DocumentId == documentId) &&
                    (keywordId == 0 || i.Keywords.Any(k => k.Id == keywordId)) &&
                    (classificationId == 0 || i.ClassificationId == classificationId))
                .Where(i => query == "" || i.Translations.Any(t => t.Title.Contains(query)))
                .OrderBy(i => i.Id)
                .ToListAsync();
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
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(new ImageDetailsViewModel
                {
                    Image = new TranslatedViewModel<Image, ImageTranslation>(image),
                    Classification = new TranslatedViewModel<Classification, ClassificationTranslation>(image.Classification),
                    Keywords = image.Keywords.ToList().Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k))
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
