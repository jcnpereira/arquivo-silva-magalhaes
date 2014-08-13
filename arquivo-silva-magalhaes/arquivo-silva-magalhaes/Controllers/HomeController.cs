using ArquivoSilvaMagalhaes.Models;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class HomeController : Controller
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

    }
}