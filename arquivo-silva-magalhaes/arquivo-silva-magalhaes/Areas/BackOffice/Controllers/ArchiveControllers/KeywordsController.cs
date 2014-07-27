using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Utilitites;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class KeywordsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Keywords
        public async Task<ActionResult> Index()
        {
            return View(await db.Keywords
                .Select(k => new KeywordViewModel
                {
                    Id = k.Id,
                    Value = k.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                })
                .ToListAsync());
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
            var model = new KeywordEditViewModel
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

            return View(new KeywordEditViewModel
                {
                    Value = keyword.Value,
                    LanguageCode = keyword.LanguageCode
                });
        }

        // GET: BackOffice/Keywords/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            return View(new KeywordEditViewModel
                {
                    KeywordId = keyword.Id,
                    LanguageCode = LanguageDefinitions.DefaultLanguage,
                    Value = keyword.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                });
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
            return View(new KeywordEditViewModel
                {
                    KeywordId = key.KeywordId,
                    Value = key.Value,
                    LanguageCode = key.LanguageCode
                });
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