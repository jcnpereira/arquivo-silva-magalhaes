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
        ArchiveDataContext _db = new ArchiveDataContext();
        
        public ActionResult Index()
        {

            var author = _db.Authors.Find(1);

            var docList = _db.Documents
                            .Where(d => d.Author.Id == author.Id)
                            .ToList();

            ViewBag.AuthorName = author.FirstName;

            ViewBag.NumberOfDocs = author.Documents.Count;
            
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