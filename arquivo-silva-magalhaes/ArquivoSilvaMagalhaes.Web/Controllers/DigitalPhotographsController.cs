using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class DigitalPhotographsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: DigitalPhotographs
        public ActionResult Index()
        {
            var digitalPhotographs = db.DigitalPhotographs.Include(d => d.Specimen);
            return View(digitalPhotographs.ToList());
        }

        // GET: DigitalPhotographs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalPhotograph = db.DigitalPhotographs.Find(id);
            if (digitalPhotograph == null)
            {
                return HttpNotFound();
            }
            return View(digitalPhotograph);
        }

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
