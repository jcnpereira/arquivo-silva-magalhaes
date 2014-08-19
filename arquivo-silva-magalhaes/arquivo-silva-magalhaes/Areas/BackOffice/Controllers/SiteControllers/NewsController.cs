using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class NewsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /News/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => 
                db.NewsItemTranslations
                  .Include(nt => nt.NewsItem)
                  .Where(nt => nt.LanguageCode == LanguageDefinitions.DefaultLanguage)
                  .OrderByDescending(nt => nt.NewsItem.CreationDate)
                  .ToPagedList(pageNumber, 10)));
        }

        // GET: /News/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsItem newsitem = await db.NewsItems.FindAsync(id);
            if (newsitem == null)
            {
                return HttpNotFound();
            }
        //    ViewBag.AreLanguagesMissing = newsitem.ReferencedNewsText.Count <= LanguageDefinitions.Languages.Count;
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewsItem newsItem)
        {
            if (ModelState.IsValid)
            {
                db.NewsItems.Add(newsItem);
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
            NewsItem newsitem = await db.NewsItems.FindAsync(id);
            if (newsitem == null)
            {
                return HttpNotFound();
            }
            return View(GenerateViewModel(newsitem));
        }

        // POST: /News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NewsItem newsitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsitem).State = EntityState.Modified;

                foreach (var t in newsitem.Translations)
                {
                    db.Entry(t).State = EntityState.Modified;
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
            NewsItem newsitem = await db.NewsItems.FindAsync(id);
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
            NewsItem newsitem = await db.NewsItems.FindAsync(id);
            db.NewsItems.Remove(newsitem);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
