using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class SearchController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();
        

        /// <summary>
        /// Forence lista paginada de elementos encontrados na pesquisa
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ActionResult Index(string searchTerm = null, int pageNumber=1)
        {
            var model =
                from c in db.CollectionTranslations
                orderby c.Title
                where (c.Title.Contains(searchTerm) || c.Provenience.Contains(searchTerm) || c.Description.Contains(searchTerm))
                select c;
            return View(model.ToPagedList(pageNumber, 10));
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
