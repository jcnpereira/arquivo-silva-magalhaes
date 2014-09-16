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
    public class ImagesController : Controller
    {
        //private ArchiveDataContext db = new ArchiveDataContext();

         private ITranslateableRepository<Image, ImageTranslation> db;

        public ImagesController()
            : this(new TranslateableGenericRepository<Image, ImageTranslation>()) { }

        public ImagesController(ITranslateableRepository<Image, ImageTranslation> db)
        {
            this.db = db;
        }

        // GET: Images
        public async Task<ActionResult> Index(int? id, int pageNumber=1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image img = new Image();

            return View((await db.Entities
                .Include(i => i.Translations)
                .Where(i => i.IsVisible)
                .Where(i => i.DocumentId == id)
                .OrderBy(i => i.Id)
                .ToListAsync())
                .Select(doc => new TranslatedViewModel<Image, ImageTranslation>(doc))
                .ToPagedList(pageNumber, 12));
        }


        // GET: Images/Details/5
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
            return View(new TranslatedViewModel<Image, ImageTranslation>(image));
        }

        //public ActionResult ViewImage(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Image image = db.Images.Find(id);
        //    if (image == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(image);
        //}

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
