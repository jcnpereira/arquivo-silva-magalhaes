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

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class EventsController : Controller
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
                return RedirectToAction("Events/Index");
            }
        }

        // GET: Events
        public async Task<ActionResult> Index( int pageNumber=1)
        {
            return View(await Task.Run(() => db.EventTranslations
            .Include(et => et.Event)
            .Where(et => (et.Event.HideAfterExpiry==false || et.Event.ExpiryDate > DateTime.Now ))
            .Where(et => et.LanguageCode == LanguageDefinitions.DefaultLanguage)
            .OrderByDescending(et => et.Event.PublishDate)
            .ToPagedList(pageNumber, 3)));
        }

        // GET: Events/Details/5
        public async Task<ActionResult> Event(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            return View(@event);
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
