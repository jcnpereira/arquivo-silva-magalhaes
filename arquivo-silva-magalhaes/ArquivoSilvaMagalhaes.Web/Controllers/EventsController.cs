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
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using PagedList.Mvc;
using ArquivoSilvaMagalhaes.ViewModels;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class EventsController : Controller
    {
        /// <summary>
        /// Associa Entidade Author às traduções existentes
        /// </summary>
        private ITranslateableRepository<Event, EventTranslation> db;

        public EventsController()
            : this(new TranslateableGenericRepository<Event, EventTranslation>()) { }

        public EventsController(ITranslateableRepository<Event, EventTranslation> db)
        {
            this.db = db;
        }


        //public ActionResult SetLanguage(string lang, string returnUrl)
        //{
        //    Response.SetCookie(new HttpCookie("lang", lang));

        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Events/Index");
        //    }
        //}

        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .Include(e => e.Partnerships)
                .Where(e => e.ExpiryDate <= DateTime.Now)
                .ToListAsync())
                .Select(b => new TranslatedViewModel<Event, EventTranslation>(b))
                .ToPagedList(pageNumber, 6));
        }

        public async Task<ActionResult> Event(int? id)
        {
            return View((await db.Entities
                .Where(e => e.Id == id)
                .ToListAsync())
                .Select(e => new TranslatedViewModel<Event, EventTranslation>(e)));
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
