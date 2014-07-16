using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
	}
}