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
using ArquivoSilvaMagalhaes.Utilitites;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class CollectionsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        public ActionResult SetLanguage(string lang, string returnUrl)
        {
            Response.SetCookie(new HttpCookie("lang", lang));

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        // GET: Collections
        public async Task<ActionResult> Index()
        {
            return View(await Task.Run(() => db.CollectionTranslations
           .Include(col => col.Collection)
           .Where(col => col.LanguageCode == LanguageDefinitions.DefaultLanguage)
           .OrderBy(col => col.Collection.EndProductionDate)));
            //return View(await db.Collections.ToListAsync());
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
