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
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Utilitites;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ClassificationsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Classifications
        public async Task<ActionResult> Index()
        {
            return View(await db.ClassificationSet.Select(c => new ClassificationViewModel 
            { 
                Id = c.Id,
                Classification = c.ClassificationTexts.FirstOrDefault(ct => ct.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
            })
            .ToListAsync());
        }

        // GET: BackOffice/Classifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.ClassificationSet.FindAsync(id);
            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(classification);
        }

        // GET: BackOffice/Classifications/Create
        public ActionResult Create()
        {
            return View(new ClassificationEditModel { LanguageCode = LanguageDefinitions.DefaultLanguage });
        }

        // POST: BackOffice/Classifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassificationEditModel model)
        {
            if (ModelState.IsValid)
            {
                var classification = new Classification();

                classification.ClassificationTexts.Add(new ClassificationText
                    {
                        LanguageCode = LanguageDefinitions.DefaultLanguage,
                        Value = model.Classfication
                    });

                db.ClassificationSet.Add(classification);
                await db.SaveChangesAsync();

                if (classification.ClassificationTexts.Count < LanguageDefinitions.Languages.Count)
                {
                    ViewBag.Id = classification.Id;
                    return View("_AddLanguagePrompt");
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BackOffice/Classifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.ClassificationSet.FindAsync(id);
            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(new ClassificationEditModel
                {
                    Id = classification.Id,
                    LanguageCode = LanguageDefinitions.DefaultLanguage,
                    Classfication = classification.ClassificationTexts.First(ct => ct.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                });
        }

        // POST: BackOffice/Classifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClassificationEditModel model)
        {
            if (ModelState.IsValid)
            {
                var classification = db.ClassificationSet.Find(model.Id);

                var t = classification.ClassificationTexts.First(ct => ct.LanguageCode == LanguageDefinitions.DefaultLanguage);

                t.Value = model.Classfication;

                db.Entry(t).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: BackOffice/Classifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.ClassificationSet.FindAsync(id);
            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(classification);
        }

        // POST: BackOffice/Classifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Classification classification = await db.ClassificationSet.FindAsync(id);
            db.ClassificationSet.Remove(classification);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult AddLanguage(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var classification = db.ClassificationSet.Find(id);

            if (classification == null) return HttpNotFound();

            var doneLanguages = classification.ClassificationTexts.Select(l => l.LanguageCode);

            return View(new ClassificationEditModel
                {
                    Id = classification.Id,
                    AvailableLanguages = LanguageDefinitions.Languages.Where(l => !doneLanguages.Contains(l))
                                                            .Select(l => new SelectListItem { Value = l, Text = LanguageDefinitions.GetLanguageName(l) }),
                });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddLanguage(ClassificationEditModel model)
        {
            if (ModelState.IsValid)
            {
                var classification = await db.ClassificationSet.FindAsync(model.Id);

                classification.ClassificationTexts.Add(new ClassificationText
                    {
                        Classification = classification,
                        LanguageCode = model.LanguageCode,
                        Value = model.Classfication
                    });

                await db.SaveChangesAsync();

                if (classification.ClassificationTexts.Count < LanguageDefinitions.Languages.Count)
                {
                    ViewBag.Id = classification.Id;
                    return View("_AddLanguagePrompt");
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult EditLanguage(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var text = db.ClassificationTextSet.Find(id);

            if (text == null) return HttpNotFound();

            var availableLanguages = db.ClassificationSet.Find(text.Classification.Id)
                                       .ClassificationTexts.Select(t => t.LanguageCode)
                                       .ToList();

            return View(new ClassificationEditModel
                {
                    Id = text.Id,
                    Classfication = text.Value,
                    LanguageCode = text.LanguageCode,
                    // AvailableLanguages = availableLanguages.Select(l => new SelectListItem { Value = l, Text = LanguageDefinitions.GetLanguageName(l) })
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLanguage(ClassificationEditModel model)
        {
            if (ModelState.IsValid)
            {
                var text = db.ClassificationTextSet.Find(model.Id);
                text.Classification = db.ClassificationSet.Find(text.Classification.Id);

                text.Value = model.Classfication;

                db.Entry(text).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return RedirectToAction("Details", new { Id = text.Classification.Id });
            }

            return View(model);
        }

        public ActionResult DeleteLanguage(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteLanguage")]
        public ActionResult DeleteLanguageConfirmed(int id)
        {
            return View();
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
