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
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ProcessesController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Processes
        public async Task<ActionResult> Index()
        {
            return View(await db.Processes
                .Select(p => new ProcessViewModel
                {
                    Id = p.Id,
                    Value = p.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                })
                .ToListAsync());
        }

        // GET: BackOffice/Processes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process p = await db.Processes.FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(new ProcessViewModel
            {
                Id = p.Id,
                Value = p.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
            });
        }

        // GET: BackOffice/Processes/Create
        public ActionResult Create()
        {
            return View(new ProcessEditViewModel
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });
        }

        // POST: BackOffice/Processes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessTranslation translation)
        {
            if (ModelState.IsValid)
            {
                var process = new Process();

                process.Translations.Add(translation);
                db.Processes.Add(process);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(new ProcessEditViewModel
                {
                    LanguageCode = translation.LanguageCode,
                    Value = translation.Value
                });
        }

        // GET: BackOffice/Processes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await db.Processes.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(new ProcessEditViewModel
                {
                    ProcessId = process.Id,
                    LanguageCode = LanguageDefinitions.DefaultLanguage,
                    Value = process.Translations.First(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                });
        }

        // POST: BackOffice/Processes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProcessTranslation translation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(translation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(new ProcessEditViewModel
            {
                LanguageCode = translation.LanguageCode,
                Value = translation.Value
            });
        }

        // GET: BackOffice/Processes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process p = await db.Processes.FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(new ProcessViewModel
            {
                Id = p.Id,
                Value = p.Translations.First(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
            });
        }

        // POST: BackOffice/Processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Process process = await db.Processes.FindAsync(id);
            db.Processes.Remove(process);
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
