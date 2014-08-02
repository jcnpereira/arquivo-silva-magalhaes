using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Utilitites;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class KeywordsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Keywords
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => db.KeywordTranslations
                .OrderBy(k => k.KeywordId)
                .ToPagedList(pageNumber, 10)));
        }

        // GET: BackOffice/Keywords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword k = await db.Keywords.FindAsync(id);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(new KeywordViewModel
            {
                Id = k.Id,
                Value = k.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
            });
        }

        // GET: BackOffice/Keywords/Create
        public ActionResult Create()
        {
            var model = new KeywordTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            };

            return View(model);
        }

        // POST: BackOffice/Keywords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KeywordTranslation keyword)
        {
            if (ModelState.IsValid)
            {
                var k = new Keyword();

                k.Translations.Add(keyword);

                db.Keywords.Add(k);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(keyword);
        }

        // GET: BackOffice/Keywords/Edit/5
        public async Task<ActionResult> Edit(int? id, string languageCode = LanguageDefinitions.DefaultLanguage)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeywordTranslation keyword = await db.KeywordTranslations.FindAsync(id, languageCode);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: BackOffice/Keywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(KeywordTranslation key)
        {
            if (ModelState.IsValid)
            {
                db.Entry(key).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(key);
        }

        // GET: BackOffice/Keywords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Keyword keyword = await db.Keywords.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(new KeywordViewModel
                {
                    Id = keyword.Id,
                    Value = keyword.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                });
        }

        // POST: BackOffice/Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Keyword keyword = await db.Keywords.FindAsync(id);
            db.Keywords.Remove(keyword);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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