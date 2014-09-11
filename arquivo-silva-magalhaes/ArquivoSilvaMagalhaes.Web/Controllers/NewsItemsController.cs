using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using PagedList;
using PagedList.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class NewsItemsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: NewsItems
        public async Task<ActionResult> Index(int? id, int pageNumber=1)
        {
            return View(await Task.Run(() => db.NewsItems.Where(news=>news.ExpiryDate<DateTime.Now).OrderByDescending(news => news.PublishDate).ToPagedList(pageNumber, 3)));
        }
      

        public async Task<ActionResult> Details(int? id)
        {

            return View(await db.NewsItems.Where(news => news.Id == id).ToListAsync());
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
