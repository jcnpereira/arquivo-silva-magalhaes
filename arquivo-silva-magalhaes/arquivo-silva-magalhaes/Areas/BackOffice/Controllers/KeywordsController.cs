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
            return View(await db.KeywordSet.ToListAsync());
                                                         
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
                var k = new Keyword
                {
                    KeywordTexts = new List<KeywordText> { new KeywordText { LanguageCode = LanguageDefinitions.DefaultLanguage, Value = keyword.Value } }
                };

                db.KeywordSet.Add(k);
                await db.SaveChangesAsync();

                if (AreLanguagesMissing(k))
                {
                    // There are languages which may be added.
                    // Ask the used if he/she wants to any.
                    ViewBag.Id = k.Id;
                    return View("_AddLanguagePrompt");
                }

                return RedirectToAction("Index");
            }

            return View(keyword);
        }

        private static bool AreLanguagesMissing(Keyword k)
        {
            // Check if we need to add more languages.
            var langCodes = k.KeywordTexts
                                  .Select(t => t.LanguageCode)
                                  .ToList();

            return LanguageDefinitions.Languages.Where(l => !langCodes.Contains(l)).Count() > 0;
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
        public async Task<ActionResult> Edit([Bind(Include="Id,Value")] Keyword key)
        {
            if (ModelState.IsValid)
            {
              /*  var keyword = db.KeywordSet.Find(keywordModel.Id);

                var keywordText = keyword.KeywordTexts.First(kt => kt.LanguageCode == LanguageDefinitions.DefaultLanguage);

                keywordText.Value = keywordModel.Value; */

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
        public async Task<ActionResult> AddLanguage(KeywordEditModel model)
        {
            if (ModelState.IsValid)
            {
                var kw = db.KeywordSet.Find(model.Id);

                kw.KeywordTexts.Add(new KeywordText
                    {
                        Value = model.Value,
                        LanguageCode = model.LanguageCode

                    });

                await db.SaveChangesAsync();
            }

            return View(model);
        }

        public ActionResult EditLanguage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var kw = db.KeywordTextSet.Find(id);

            if (kw == null)
            {
                return HttpNotFound();
            }

            return View(new KeywordEditModel
                {
                    Id = kw.Id,
                    Value = kw.Value,
                    LanguageCode = kw.LanguageCode
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLanguage(KeywordEditModel model)
        {
            if (ModelState.IsValid)
            {
                var kw = db.KeywordTextSet.Find(model.Id);

                kw.Value = model.Value;

                db.Entry(kw).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteLanguage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var kw = db.KeywordTextSet.Find(id);

            if (kw == null)
            {
                return HttpNotFound();
            }

            return View(kw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLanguage(int id)
        {
            var kw = db.KeywordTextSet.Find(id);

            if (kw == null) return HttpNotFound();

            db.KeywordTextSet.Remove(kw);

            db.SaveChanges();

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
