using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class KeywordsController : ArchiveControllerBase
    {
        private ITranslateableRepository<Keyword, KeywordTranslation> db;

        public KeywordsController()
            : this(new TranslateableGenericRepository<Keyword, KeywordTranslation>()) { }

        public KeywordsController(ITranslateableRepository<Keyword, KeywordTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Keywords
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .OrderBy(b => b.Id)
                .ToListAsync())
                .Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k))
                .ToPagedList(pageNumber, 10));
        }

        // GET: BackOffice/Keywords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var k = await db.GetByIdAsync(id);

            if (k == null)
            {
                return HttpNotFound();
            }

            k.Translations = k.Translations.ToList();
            return View(k);
        }

        // GET: BackOffice/Keywords/Create
        public ActionResult Create()
        {
            var model = new KeywordTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_KeywordFields", model);
            }
            else
            {
                return View(model);
            }
        }

        // POST: BackOffice/Keywords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KeywordTranslation kt)
        {
            if (ModelState.IsValid)
            {
                var keyword = new Keyword();
                keyword.Translations.Add(kt);

                db.Add(keyword);
                await db.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                {
                    return Json((await db.GetAllAsync())
                        .OrderBy(k => k.Id)
                        .Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k))
                        .Select(ktr => new
                        {
                            value = ktr.Entity.Id.ToString(),
                            text = ktr.Translation.Value
                        })
                        .ToList());
                }

                return RedirectToAction("Index");
            }

            return View(kt);
        }

        // GET: BackOffice/Keywords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var k = await db.GetByIdAsync(id);

            if (k == null)
            {
                return HttpNotFound();
            }

            k.Translations = k.Translations.ToList();

            return View(k);
        }

        // POST: BackOffice/Keywords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                foreach (var t in keyword.Translations)
                {
                    db.UpdateTranslation(t);
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: BackOffice/Keywords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Keyword keyword = await db.GetByIdAsync(id);

            if (keyword == null)
            {
                return HttpNotFound();
            }

            return View(keyword);
        }

        // POST: BackOffice/Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.RemoveByIdAsync(id);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #region Translation Actions
        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var k = await db.GetByIdAsync(id);

            if (k == null)
            {
                return HttpNotFound();
            }

            ViewBag.Languages =
                LanguageDefinitions.GenerateAvailableLanguageDDL(k.Translations.Select(t => t.LanguageCode));

            var model = new KeywordTranslation
            {
                KeywordId = k.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(KeywordTranslation translation)
        {
            if (ModelState.IsValid)
            {
                db.AddTranslation(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
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
        #endregion

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