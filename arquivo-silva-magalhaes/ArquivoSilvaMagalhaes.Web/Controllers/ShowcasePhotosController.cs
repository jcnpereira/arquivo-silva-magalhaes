using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Threading.Tasks;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using PagedList.Mvc;
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ShowcasePhotosController : Controller
    {
        private ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db;

        public ShowcasePhotosController()
            : this(new TranslateableGenericRepository<ShowcasePhoto, ShowcasePhotoTranslation>()) { }

        public ShowcasePhotosController(ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db)
        {
            this.db = db;
        }

        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .Include(sp => sp.Image)
                .Where(sp => sp.VisibleSince <= DateTime.Now)
                .ToListAsync())
                .Select(b => new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>(b))
                .ToPagedList(pageNumber, 6));
        }

        // GET: /ShowCasePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasephoto = await db.GetByIdAsync(id);
            if (showcasephoto == null || showcasephoto.VisibleSince >= DateTime.Now)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>(showcasephoto));
        }

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
