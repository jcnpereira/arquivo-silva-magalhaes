using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class NewsController : SiteControllerBase
    {
        private ITranslateableRepository<NewsItem, NewsItemTranslation> db;

        public NewsController()
            : this(new TranslateableRepository<NewsItem, NewsItemTranslation>())
        {
        }

        public NewsController(ITranslateableRepository<NewsItem, NewsItemTranslation> db)
        {
            this.db = db;
        }

        // GET: /News/
        public async Task<ActionResult> Index(int pageNumber = 1, string query = "")
        {
            var model = await db.Entities
                .Include(n => n.Translations)
                .Where(e => e.Translations.Any(t => t.Title.Contains(query)))
                .OrderBy(b => b.PublishDate)
                .Select(e => new TranslatedViewModel<NewsItem, NewsItemTranslation>
                {
                    Entity = e
                })
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.Query = query;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        // GET: /News/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newsitem = await db.GetByIdAsync(id);

            if (newsitem == null)
            {
                return HttpNotFound();
            }
            return View(newsitem);
        }

        // GET: /News/Create
        public ActionResult Create()
        {
            var item = new NewsItem();

            item.Translations.Add(new NewsItemTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            return View(GenerateViewModel(item));
        }

        // POST: /News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewsItem newsItem)
        {
            if (ModelState.IsValid)
            {
                db.Add(newsItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(newsItem));
        }

        // GET: /News/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItem newsitem = await db.GetByIdAsync(id);
            if (newsitem == null)
            {
                return HttpNotFound();
            }
            return View(GenerateViewModel(newsitem));
        }

        // POST: /News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NewsItem newsitem)
        {
            if (ModelState.IsValid)
            {
                db.Update(newsitem);

                foreach (var t in newsitem.Translations)
                {
                    db.UpdateTranslation(t);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(GenerateViewModel(newsitem));
        }

        // GET: /News/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItem newsitem = await db.GetByIdAsync(id);
            if (newsitem == null)
            {
                return HttpNotFound();
            }
            return View(newsitem);
        }

        // POST: /News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.RemoveByIdAsync(id);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newsItem = await db.GetByIdAsync(id);

            if (newsItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                newsItem.Translations.Select(t => t.LanguageCode).ToArray());

            return View(new NewsItemTranslation
            {
                NewsItemId = newsItem.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(NewsItemTranslation translation)
        {
            var item = await db.GetByIdAsync(translation.NewsItemId);

            if (ModelState.IsValid)
            {
                item.Translations.Add(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                item.Translations.Select(t => t.LanguageCode).ToArray());

            return View(translation);
        }

        public async Task<ActionResult> DeleteTranslation(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode) || languageCode == LanguageDefinitions.DefaultLanguage)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            return View(tr);
        }

        [HttpPost]
        [ActionName("DeleteTranslation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTranslationConfirmed(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            await db.RemoveTranslationByIdAsync(id, languageCode);

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private NewsItemViewModel GenerateViewModel(NewsItem item)
        {
            var model = new NewsItemViewModel
            {
                NewsItem = item
            };

            return model;
        }

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