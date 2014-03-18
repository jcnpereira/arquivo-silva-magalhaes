using ArquivoSilvaMagalhaes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new ArchiveDataContext())
            {
                var authors = db.Authors
                               .Where(a => a.Id == 1)
                               .Select(a => a)
                               .ToList();

                if (authors.Count != 0)
                {
                    var author = authors[0];
                    ViewBag.AuthorName = String.Format("{0}, {1}", author.LastName, author.FirstName);
                }
                else
                {
                    ViewBag.AuthorName = "None";
                }
                
            }

            return View();
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
    }
}