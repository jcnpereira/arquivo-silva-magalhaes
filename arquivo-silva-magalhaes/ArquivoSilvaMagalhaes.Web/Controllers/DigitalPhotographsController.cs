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

        /// <summary>
        /// Fornece uma lista de fotografias digitais
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var digitalPhotographs = db.DigitalPhotographs.Include(d => d.Specimen);
            return View(digitalPhotographs.ToList());
        }

       /// <summary>
       /// Fornece os atributos de uma determinada fotografia digital
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
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

        /// <summary>
        /// Actualização à base de dados
        /// </summary>
        /// <param name="disposing"></param>
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
