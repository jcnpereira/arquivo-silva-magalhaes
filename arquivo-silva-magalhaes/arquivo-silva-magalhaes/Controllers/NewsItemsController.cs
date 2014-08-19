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

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class NewsItemsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: NewsItems
        public async Task<ActionResult> Index()
        {
            return View(await db.NewsItemTranslations.ToListAsync());
        }

        // GET: NewsItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //NewsItem newsItem = await db.NewsItems.FindAsync(id);
            //if (newsItem == null)
            //{
            //    return HttpNotFound();
            //}
            return View(await db.NewsItemTranslations.ToListAsync());
        }

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
       
    }
}
