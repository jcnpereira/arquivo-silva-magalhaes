using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ArquivoSilvaMagalhaes.Common;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class PartnershipsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        /// <summary>
        /// Fornece lista paginada de parcerias por oredem descendente ao seu id
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await db.Partnerships
                .OrderBy(p => p.Id)
                .ToPagedListAsync(pageNumber, 12));
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
