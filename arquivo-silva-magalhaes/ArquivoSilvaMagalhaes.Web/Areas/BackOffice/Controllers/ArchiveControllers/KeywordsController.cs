using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class KeywordsController : ArchiveControllerBase
    {
        private ITranslateableRepository<Keyword, KeywordTranslation> _db;

        public KeywordsController(ITranslateableRepository<Keyword, KeywordTranslation> db)
        {
            this._db = db;
        }

        public KeywordsController() : this(new TranslateableGenericRepository<Keyword, KeywordTranslation>())
        {

        }

        // GET: BackOffice/Keywords
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await _db.GetAllAsync())
                .OrderBy(k => k.Id)
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

            var k = await _db.GetByIdAsync(id);

            if (k == null)
            {
                return HttpNotFound();
            }

            k.Translations = k.Translations.ToList();
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

                _db.Add(keyword);
                await _db.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                {
                    return Json((await _db.GetAllAsync())
                        .OrderBy(k => k.Id)
                        .Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k))
                        .Select(ktr => new
                        {
                            value = ktr.Entity.ToString(),
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

            var k = await _db.GetByIdAsync(id);

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
                    _db.UpdateTranslation(t);
                }

                await _db.SaveChangesAsync();

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

            Keyword keyword = await _db.GetByIdAsync(id);

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
            await _db.RemoveByIdAsync(id);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var k = await _db.GetByIdAsync(id);

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
                _db.AddTranslation(translation);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(translation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _db != null)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}