using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Web.I18n;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ProcessesController : ArchiveControllerBase
    {
        private ITranslateableRepository<Process, ProcessTranslation> db;

        public ProcessesController(ITranslateableRepository<Process, ProcessTranslation> db)
        {
            this.db = db;
        }

        public ProcessesController()
            : this(new TranslateableRepository<Process, ProcessTranslation>())
        {
        }

        // GET: BackOffice/Processes
        public async Task<ActionResult> Index(int pageNumber = 1, string query = "")
        {
            var model = await db.Entities
                .Include(p => p.Translations)
                .Where(c => c.Translations.Any(t => t.Value.Contains(query)))
                .OrderBy(c => c.Id)
                .Select(c => new TranslatedViewModel<Process, ProcessTranslation>
                {
                    Entity = c
                })
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.Query = query;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        // GET: BackOffice/Processes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process p = await db.GetByIdAsync(id);

            if (p == null)
            {
                return HttpNotFound();
            }

            p.Translations = p.Translations.ToList();

            return View(p);
        }

        // GET: BackOffice/Processes/Create
        public ActionResult Create()
        {
            var model = new ProcessTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            };

            return View(model);
        }

        // POST: BackOffice/Processes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessTranslation pt)
        {
            if (DoesProcessExist(pt))
            {
                ModelState.AddModelError("Value", ProcessStrings.Validation_AlreadyExists);
            }

            if (ModelState.IsValid)
            {
                var process = new Process();
                process.Translations.Add(pt);

                db.Add(process);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(pt);
        }

        // GET: BackOffice/Processes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await db.GetByIdAsync(id);

            if (process == null)
            {
                return HttpNotFound();
            }
            process.Translations = process.Translations.ToList();
            return View(process);
        }

        // POST: BackOffice/Processes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Process process)
        {
            for (var i = 0; i < process.Translations.Count; i++)
            {
                var pt = process.Translations[i];
                if (DoesProcessExist(pt))
                {
                    ModelState.AddModelError("Translations[" + i + "].Value",
                        ProcessStrings.Validation_AlreadyExists);
                }
            }

            if (ModelState.IsValid)
            {
                foreach (var translation in process.Translations)
                {
                    db.UpdateTranslation(translation);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(process);
        }

        // GET: BackOffice/Processes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process p = await db.GetByIdAsync(id);

            if (p == null)
            {
                return HttpNotFound();
            }

            p.Translations = p.Translations.ToList();
            return View(p);
        }

        // POST: BackOffice/Processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Process process = await db.GetByIdAsync(id);
            db.Remove(process);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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

            var model = new ProcessTranslation
            {
                ProcessId = k.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(ProcessTranslation translation)
        {
            if (DoesProcessExist(translation))
            {
                ModelState.AddModelError("Value", ProcessStrings.Validation_AlreadyExists);
            }

            if (ModelState.IsValid)
            {
                db.AddTranslation(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(translation);
        }

        #region Translation Actions
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

        private bool DoesProcessExist(ProcessTranslation p)
        {
            return db.Set<ProcessTranslation>()
                .Any(t =>
                    t.LanguageCode == p.LanguageCode &&
                    t.Value == p.Value &&
                    t.ProcessId != p.ProcessId);
        }

        public ActionResult AuxAdd()
        {
            var model = new ProcessTranslation { LanguageCode = LanguageDefinitions.DefaultLanguage };

            return PartialView("_ProcessFields", model);
        }

        [HttpPost]
        public async Task<ActionResult> AuxAdd(ProcessTranslation t)
        {
            var cl = db.Entities
                .FirstOrDefault(c => c.Translations.Any(ct =>
                    ct.LanguageCode == t.LanguageCode &&
                    ct.Value == t.Value &&
                    ct.ProcessId != t.ProcessId)
                );

            if (cl == null)
            {
                cl = new Process();
                cl.Translations.Add(t);

                db.Add(cl);
                await db.SaveChangesAsync();
            }

            return Json((await db.Entities
                .OrderBy(ct => ct.Id)
                .ToListAsync())
                .Select(ct => new TranslatedViewModel<Process, ProcessTranslation>(ct))
                .Select(ct => new
                {
                    value = ct.Entity.Id.ToString(),
                    text = ct.Translation.Value,
                    selected = ct.Entity.Id == cl.Id
                })
                .ToList());
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