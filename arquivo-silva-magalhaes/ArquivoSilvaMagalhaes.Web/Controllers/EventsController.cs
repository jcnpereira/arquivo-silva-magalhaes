using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class EventsController : Controller
    {
        /// <summary>
        /// Associa Entidade Author às traduções existentes
        /// </summary>
        private ITranslateableRepository<Event, EventTranslation> db;

        public EventsController()
            : this(new TranslateableRepository<Event, EventTranslation>()) { }

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
                .Where(e => e.ExpiryDate >= DateTime.Now || !e.HideAfterExpiry)
                .OrderByDescending(e => e.PublishDate)
                .ToListAsync())
                .Select(b => new TranslatedViewModel<Event, EventTranslation>(b))
                .ToPagedList(pageNumber, 10));
        }

        public async Task<ActionResult> Event(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var e = await db.GetByIdAsync(id);

            if (e.ExpiryDate >= DateTime.Now && e.HideAfterExpiry)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            e.Attachments = e.Attachments.ToList();

            return View(new TranslatedViewModel<Event, EventTranslation>(e));
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
