using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class HomeController : Controller
    {
        ArchiveDataContext _db = new ArchiveDataContext();
        
        public ActionResult Index()
        {
            var collections = _db.CollectionSet
                .Where(c => c.IsVisible)
                .ToList();

            return View(collections);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Author()
        {
            var author = _db.AuthorSet.First(a => a.LastName.Contains("Maga"));

            return Content(author.Biography);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}