﻿using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
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
            : this(new TranslateableGenericRepository<Process, ProcessTranslation>())
        {
        }

        // GET: BackOffice/Processes
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .OrderBy(b => b.Id)
                .ToListAsync())
                .Select(p => new TranslatedViewModel<Process, ProcessTranslation>(p))
                .ToPagedList(pageNumber, 10));
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

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProcessFields", model);
            }

            return View(model);
        }

        // POST: BackOffice/Processes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessTranslation pt)
        {
            if (ModelState.IsValid)
            {
                var process = new Process();
                process.Translations.Add(pt);

                db.Add(process);

                await db.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                {
                    var result = new List<object>
                    {
                        new {
                            text = UiPrompts.ChooseOne,
                            value = ""
                        }
                    };

                    result.AddRange((await db.GetAllAsync())
                          .OrderBy(ptr => ptr.Id)
                          .Select(p => new TranslatedViewModel<Process, ProcessTranslation>(p))
                          .Select(ptr => new
                          {
                              text = ptr.Translation.Value,
                              value = ptr.Entity.Id.ToString()
                          }));

                    return Json(result);
                }

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

            var model = new KeywordTranslation
            {
                KeywordId = k.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(ProcessTranslation translation)
        {
            if (ModelState.IsValid)
            {
                db.AddTranslation(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(translation);
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