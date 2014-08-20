using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class SearchController : FrontOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();
        // GET: Search
        public ActionResult Index(string searchTerm = null)
        {
            var model =
                from c in db.CollectionTranslations
                orderby c.Title
                where (c.Title.Contains(searchTerm) || c.Provenience.Contains(searchTerm) || c.Description.Contains(searchTerm))
                select c;
            return View(model);
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
