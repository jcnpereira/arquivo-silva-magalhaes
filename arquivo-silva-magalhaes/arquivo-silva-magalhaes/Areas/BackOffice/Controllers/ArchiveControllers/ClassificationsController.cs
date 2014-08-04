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
using PagedList;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ClassificationsController : BackOfficeController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/Classifications
        public async Task<ActionResult> Index(
            int pageNumber = 1,
            string query = null, 
            string orderByColumn = null)
        {
            return View((await _db.Classifications
                .ToListAsync())
                .Select(c => new ClassificationViewModel(c))
                .ToPagedList(pageNumber, 10));
        }

        // GET: BackOffice/Classifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var classification = await _db.Classifications.FindAsync(id);

            classification.Translations = classification.Translations.ToList();

            if (classification == null)
            {
                return HttpNotFound();
            }

            return View(classification);
        }

        // GET: BackOffice/Classifications/Create
        public ActionResult Create()
        {
            return View(new ClassificationTranslation { LanguageCode = LanguageDefinitions.DefaultLanguage });
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

                _db.Classifications.Add(c);
                await _db.SaveChangesAsync();

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

            Classification classification = await _db.Classifications.FindAsync(id);

            Debug.WriteLine(classification.Translations.Count);

            if (classification == null)
            {
                return HttpNotFound();
            }

            return View(classification);
        }

        // POST: BackOffice/Classifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Classification classification)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(classification).State = EntityState.Modified;

                foreach (var t in classification.Translations)
                {
                    _db.Entry(t).State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();
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
            Classification classification = await _db.Classifications.FindAsync(id);
            classification.Translations = classification.Translations.ToList();

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
            Classification classification = await _db.Classifications.FindAsync(id);
            _db.Classifications.Remove(classification);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
