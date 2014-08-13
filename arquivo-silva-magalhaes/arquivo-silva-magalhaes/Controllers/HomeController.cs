using ArquivoSilvaMagalhaes.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Threading;
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class HomeController : FrontOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult History()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }


        public ActionResult Collections()
        {
            var model = db.Collections
                .Where(c => c.IsVisible)
                .Include(c => c.Translations)
                .ToList()
                .Select(c => new CollectionViewModel(c))
                .ToList();




            return View(model);
        }
    }
}