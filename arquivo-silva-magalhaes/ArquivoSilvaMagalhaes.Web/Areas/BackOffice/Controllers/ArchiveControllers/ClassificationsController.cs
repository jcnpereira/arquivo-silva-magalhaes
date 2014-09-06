using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ClassificationsController : ArchiveControllerBase
    {
        private ITranslateableRepository<Classification, ClassificationTranslation> db;

        public ClassificationsController()
            : this(new TranslateableGenericRepository<Classification, ClassificationTranslation>())
        {
        }

        public ClassificationsController(ITranslateableRepository<Classification, ClassificationTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Classifications
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .OrderBy(b => b.Id)
                .ToListAsync())
                .Select(c => new TranslatedViewModel<Classification, ClassificationTranslation>(c))
                .ToPagedList(pageNumber, 10));
        }

        // GET: BackOffice/Classifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classification = await db.GetByIdAsync(id);

            if (classification == null)
            {
                return HttpNotFound();
            }

            classification.Translations = classification.Translations.ToList();

            return View(classification);
        }

        // GET: BackOffice/Classifications/Create
        public ActionResult Create()
        {
            var model = new ClassificationTranslation { LanguageCode = LanguageDefinitions.DefaultLanguage };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ClassificationFields", model);
            }
            else
            {
                return View(model);
            }
        }

        // POST: BackOffice/Classifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassificationTranslation classification)
        {
            if (ModelState.IsValid)
            {
                var c = new Classification();

                c.Translations.Add(classification);

                db.Add(c);
                await db.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                {
                    return Json((await db.GetAllAsync())
                                  .OrderBy(ct => ct.Id)
                                  .Select(ct => new TranslatedViewModel<Classification, ClassificationTranslation>(ct))
                                  .Select(ct => new
                                  {
                                      value = ct.Entity.Id.ToString(),
                                      text = ct.Translation.Value
                                  })
                                  .ToList());
                }

                return RedirectToAction("Index");
            }

            return View(classification);
        }

        // GET: BackOffice/Classifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classification classification = await db.GetByIdAsync(id);

            if (classification == null)
            {
                return HttpNotFound();
            }

            return View(classification);
        }

        // POST: BackOffice/Classifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Classification classification)
        {
            if (ModelState.IsValid)
            {
                foreach (var t in classification.Translations)
                {
                    db.UpdateTranslation(t);
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(classification);
        }

        // GET: BackOffice/Classifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.GetByIdAsync(id);

            if (classification == null)
            {
                return HttpNotFound();
            }

            classification.Translations = classification.Translations.ToList();
            return View(classification);
        }

        // POST: BackOffice/Classifications/Delete/5
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

            var c = await db.GetByIdAsync(id);

            if (c == null)
            {
                return HttpNotFound();
            }

            ViewBag.Languages = LanguageDefinitions
                .GenerateAvailableLanguageDDL(c.Translations.Select(t => t.LanguageCode).ToList());

            return View(new ClassificationTranslation
                {
                    ClassificationId = c.Id
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(ClassificationTranslation translation)
        {
            if (ModelState.IsValid)
            {
                db.AddTranslation(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var c = await db.GetByIdAsync(translation.ClassificationId);

            if (c != null)
            {
                ViewBag.Languages = LanguageDefinitions
                    .GenerateAvailableLanguageDDL(c.Translations.Select(t => t.LanguageCode).ToList());
            }

            return View(translation);
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