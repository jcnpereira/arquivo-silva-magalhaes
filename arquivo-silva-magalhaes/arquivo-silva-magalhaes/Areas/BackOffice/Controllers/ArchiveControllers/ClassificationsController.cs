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
using System.Diagnostics;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ClassificationsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Classifications
        public async Task<ActionResult> Index()
        {
            return View(await db.Classifications
                .Select(c => new ClassificationViewModel(c))
                .ToListAsync());
        }

        // GET: BackOffice/Classifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classification = await db.Classifications.FindAsync(id);

            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(new ClassificationViewModel
            {
                Id = classification.Id,
                Value = classification.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
            });
        }

        // GET: BackOffice/Classifications/Create
        public ActionResult Create()
        {
            return View(new ClassificationEditViewModel { LanguageCode = LanguageDefinitions.DefaultLanguage });
        }

        // POST: BackOffice/Classifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassificationTranslation classification)
        {
            if (ModelState.IsValid)
            {
                var c = new Classification();

                c.Translations.Add(classification);

                db.Classifications.Add(c);
                await db.SaveChangesAsync();

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

            Classification classification = await db.Classifications.FindAsync(id);

            Debug.WriteLine(classification.Translations.Count);

            if (classification == null)
            {
                return HttpNotFound();
            }

            return View(new ClassificationEditViewModel
                {
                    ClassificationId = classification.Id,
                    LanguageCode = LanguageDefinitions.DefaultLanguage,
                    Value = classification.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                });
        }

        // POST: BackOffice/Classifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClassificationTranslation classification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(new ClassificationEditViewModel
                {
                    ClassificationId = classification.ClassificationId,
                    Value = classification.Value,
                    LanguageCode = classification.Value
                });
        }

        // GET: BackOffice/Classifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.Classifications.FindAsync(id);
            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(new ClassificationViewModel
                {
                    Id = classification.Id,
                    Value = classification.Translations.FirstOrDefault(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                });
        }

        // POST: BackOffice/Classifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Classification classification = await db.Classifications.FindAsync(id);
            db.Classifications.Remove(classification);
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
