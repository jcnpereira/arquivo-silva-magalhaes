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
    public class CollectionsController : FrontOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        
        // GET: Collections
        public async Task<ActionResult> Index()
        {
            return View((await db.Collections
                            .Include(col => col.Authors)
                            .OrderBy(col => col.EndProductionDate)
                            .ToListAsync())
                            .Select(col => new CollectionViewModel(col)));
        }

        public async Task<ActionResult> Docs(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: Collections/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
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
