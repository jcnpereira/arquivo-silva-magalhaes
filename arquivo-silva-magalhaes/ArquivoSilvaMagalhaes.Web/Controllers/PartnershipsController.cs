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

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class PartnershipsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: /Partnerships/
        public async Task<ActionResult> Index(int pageNumber=1)
        {
            return View(await Task.Run(() => db.Partnerships
                .OrderByDescending(p=>p.Id)
                .ToPagedList(pageNumber,6)));
        }

        // GET: /Partnerships/Details/5
       public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           }
           Partnership partnership = await db.Partnerships.FindAsync(id);
           if (partnership == null)
           {
               return HttpNotFound();
           }
           return View(partnership);
        }
    }
}
