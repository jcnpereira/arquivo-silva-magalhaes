using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class HomeController : BackOfficeController
    {
        //
        // GET: /BackOffice/Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
	}
}