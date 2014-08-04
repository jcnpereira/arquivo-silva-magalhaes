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
using PagedList;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ProcessesController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Processes
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => db.Processes
                .OrderBy(p => p.Id)
                .ToPagedList(pageNumber, 10)));
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
            return View(p);
        }

        // GET: BackOffice/Processes/Create
        public ActionResult Create()
        {
            var model = new Process();
            model.Translations.Add(new ProcessTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

            return View(model);
        }

        // POST: BackOffice/Processes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Process process)
        {
            if (ModelState.IsValid)
            {
                db.Processes.Add(process);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(process);
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
            process.Translations = process.Translations.ToList();
            return View(process);
        }

        // POST: BackOffice/Processes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Process process)
        {
            if (ModelState.IsValid)
            {
                foreach (var translation in process.Translations)
                {
                    db.Entry(translation).State = EntityState.Modified; 
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
            Process p = await db.Processes.FindAsync(id);
            
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
