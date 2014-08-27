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
        private ITranslateableEntityRepository<Keyword, KeywordTranslation> db;

        public KeywordsController(ITranslateableEntityRepository<Keyword, KeywordTranslation> db)
        {
            this.db = db;
        }

        public KeywordsController() : this(new TranslateableGenericRepository<Keyword, KeywordTranslation>())
        {

        }

        // GET: BackOffice/Keywords
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.GetAll())
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

            var k = await db.GetById(id);

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

                db.Add(keyword);
                await db.SaveChanges();

                if (Request.IsAjaxRequest())
                {
                    return Json((await db.GetAll())
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

            var k = await db.GetById(id);

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

                await db.SaveChanges();

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

            Keyword keyword = await db.GetById(id);

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
            await db.RemoveById(id);
            await db.SaveChanges();

            return RedirectToAction("Index");
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