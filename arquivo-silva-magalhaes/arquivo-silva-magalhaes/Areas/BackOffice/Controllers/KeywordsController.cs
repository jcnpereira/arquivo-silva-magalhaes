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
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Utilitites;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class KeywordsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Keywords
        public async Task<ActionResult> Index()
        {
            return View(await db.KeywordSet.Select(k => new KeywordViewModel { Id = k.Id, Keyword = k.KeywordTexts
                                                                                                     .First(kt => kt.LanguageCode == LanguageDefinitions.DefaultLanguage).Value })
                                                                                                     .ToListAsync());
        }

        // GET: BackOffice/Keywords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.KeywordSet.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // GET: BackOffice/Keywords/Create
        public ActionResult Create()
        {
            var model = new KeywordEditModel
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            };

            return View(model);
        }

        // POST: BackOffice/Keywords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KeywordEditModel keyword)
        {
            if (ModelState.IsValid)
            {
                db.KeywordSet.Add(new Keyword
                {
                    KeywordTexts = new List<KeywordText> { new KeywordText { LanguageCode = LanguageDefinitions.DefaultLanguage, Value = keyword.Keyword } }
                });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(keyword);
        }

        // GET: BackOffice/Keywords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.KeywordSet.FindAsync(id);
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
        public async Task<ActionResult> Edit(KeywordEditModel keywordModel)
        {
            if (ModelState.IsValid)
            {
                var keyword = db.KeywordSet.Find(keywordModel.Id);

                var keywordText = keyword.KeywordTexts.First(kt => kt.LanguageCode == LanguageDefinitions.DefaultLanguage);

                keywordText.Value = keywordModel.Keyword;

                db.Entry(keywordText).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(keywordModel);
        }

        // GET: BackOffice/Keywords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Keyword keyword = await db.KeywordSet.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        
        public async Task<ActionResult> AddLanguage(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Keyword keyword = await db.KeywordSet.FindAsync(id);

            if (keyword == null) return HttpNotFound();

            if (keyword.KeywordTexts.Count == LanguageDefinitions.Languages.Count)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var codes = keyword.KeywordTexts.Select(kt => kt.LanguageCode)
                                            .ToList();

            var notDoneLanguages = LanguageDefinitions.Languages.Where(l => !codes.Contains(l))
                                                                .ToList();

            var kwModel = new KeywordEditModel
            {
                AvailableLanguages = notDoneLanguages
                                        .Select(l => new SelectListItem
                                                {
                                                    Text = LanguageDefinitions.GetLanguageName(l),
                                                    Value = l
                                                })
            };

            return View(kwModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLanguage(KeywordEditModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View(model);
        }

        public ActionResult EditLanguage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLanguage()
        {
            return View();
        }

        public ActionResult DeleteLanguage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLanguage()
        {
            return View();
        }

        // POST: BackOffice/Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Keyword keyword = await db.KeywordSet.FindAsync(id);
            db.KeywordSet.Remove(keyword);
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
