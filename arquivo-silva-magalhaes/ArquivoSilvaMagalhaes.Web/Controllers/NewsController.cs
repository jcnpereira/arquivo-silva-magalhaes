using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Common;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class NewsController : Controller
    {
        private ITranslateableRepository<NewsItem, NewsItemTranslation> db;

        public NewsController()
            : this(new TranslateableRepository<NewsItem, NewsItemTranslation>()) { }

        public NewsController(ITranslateableRepository<NewsItem, NewsItemTranslation> db)
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
            var model = await db.Entities
                .Where(n => n.PublishDate <= DateTime.Now && n.ExpiryDate >= DateTime.Now || n.HideAfterExpiry == false)
                .OrderByDescending(n => n.PublishDate)
                .Select(b => new TranslatedViewModel<NewsItem, NewsItemTranslation>
                {
                    Entity = b
                })
                .ToPagedListAsync(pageNumber, 10);

            return View(model);
        }

        /// <summary>
        /// Forence o conteúdo de uma determinada notíca
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            var newsItem = await db.GetByIdAsync(id);

            if (newsItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(new TranslatedViewModel<NewsItem, NewsItemTranslation>(newsItem));
        }

        /// <summary>
        /// Actualização à base de dados
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
