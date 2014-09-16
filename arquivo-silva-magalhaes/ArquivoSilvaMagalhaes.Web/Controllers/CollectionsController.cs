using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using ArquivoSilvaMagalhaes.Common;


namespace ArquivoSilvaMagalhaes.Controllers
{
    public class CollectionsController : Controller
    {
        //private ArchiveDataContext db = new ArchiveDataContext();


        private ITranslateableRepository<Collection, CollectionTranslation> db;

        public CollectionsController()
            : this(new TranslateableGenericRepository<Collection, CollectionTranslation>()) { }

        public CollectionsController(ITranslateableRepository<Collection, CollectionTranslation> db)
        {
            this.db = db;
        }

        // GET: Collections
        public async Task<ActionResult> Index()
        {
            return View((await db.Entities
                            .Include(col => col.Authors)
                            .OrderByDescending(col => col.EndProductionDate)
                            .Where(col => col.IsVisible)
                            .ToListAsync())
                            .Select(col => new TranslatedViewModel<Collection, CollectionTranslation>(col)));
        }

        public async Task<ActionResult> Details(int? id)
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

