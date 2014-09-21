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
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using PagedList.Mvc;
using ArquivoSilvaMagalhaes.ViewModels;


namespace ArquivoSilvaMagalhaes.Controllers
{
    public class NewsItemsController : Controller
    {
       /// <summary>
        /// Associa Entidade NewsItems às traduções existentes
       /// </summary>
         private ITranslateableRepository<NewsItem, NewsItemTranslation> db;

        public NewsItemsController()
            : this(new TranslateableGenericRepository<NewsItem, NewsItemTranslation>()) { }

        public NewsItemsController(ITranslateableRepository<NewsItem, NewsItemTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece lista paginada de notícias que estejam dentro do intervalo de tempo definido
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .Include(n => n.Attachments)
                .Where(n => n.PublishDate <= DateTime.Now)
                .Where(n => n.ExpiryDate >= DateTime.Now || n.HideAfterExpiry==false)
                .ToListAsync())
                .Select(b => new TranslatedViewModel<NewsItem, NewsItemTranslation>(b))
                .ToPagedList(pageNumber, 6));
        }

        /// <summary>
        /// Forence o conteúdo de uma determinada notíca
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            return View((await db.Entities
                .Include(n=>n.Translations)
                .Where(n => n.Id == id)
                .ToListAsync())
                .Select(b => new TranslatedViewModel<NewsItem, NewsItemTranslation>(b)));
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
