using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Common;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ShowcaseController : Controller
    {
        /// <summary>
        /// Associa entidade ShowcasePhoto às traduções existentes
        /// </summary>
        private ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db;

        public ShowcaseController()
            : this(new TranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation>()) { }

        public ShowcaseController(ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece uma fotografia em destaque por página colocando a mais recente na primeira página
        /// e as outras nas seguintes dor ordem descendente
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Old(int pageNumber = 1)
        {
            var model = await db.Entities
                .Include(sp => sp.Image)
                .OrderByDescending(sp => sp.VisibleSince)
                .Where(sp => sp.VisibleSince <= DateTime.Now && (sp.HideAt == null || sp.HideAt.Value > DateTime.Now))
                .Select(b => new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>
                {
                    Entity = b
                })
                .ToPagedListAsync(pageNumber, 10);

            return View(model);
        }

        public async Task<ActionResult> Index()
        {// Get the newest one.
            ShowcasePhoto showcasephoto = await db.Entities
                .Include(sp => sp.Image)
                .Where(sp => sp.VisibleSince <= DateTime.Now)
                .OrderByDescending(sp => sp.VisibleSince)
                .FirstOrDefaultAsync(sp => sp.HideAt == null || sp.HideAt.Value > DateTime.Now);

            if (showcasephoto == null)
            {
                return HttpNotFound();
            }

            return View("Article", new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>(showcasephoto));
        }

        /// <summary>
        /// Fornece o nome do Comentador e o seu email caso seja permitido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Article(int? id)
        {
            ShowcasePhoto sp;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                // Display the one with said ID.

                sp = await db.Entities
                    .Include(s => s.Image)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (sp == null || sp.VisibleSince > DateTime.Now || (sp.HideAt.HasValue && sp.HideAt.Value <= DateTime.Now))
                {
                    return HttpNotFound();
                }
            }

            return View("Article", new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>(sp));
        }

        /// <summary>
        /// Actualização à base de dados
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
