using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models.SiteViewModels;
using ArquivoSilvaMagalhaes.Models;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class NewsViewController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /NewsView/
        public async Task<ActionResult> Index()
        {
            return View(await db.NewsItemViewModels.ToListAsync());
        }

        // GET: /NewsView/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItemViewModels newsitemviewmodels = await db.NewsItemViewModels.FindAsync(id);
            if (newsitemviewmodels == null)
            {
                return HttpNotFound();
            }
            return View(newsitemviewmodels);
        }
    }
}
