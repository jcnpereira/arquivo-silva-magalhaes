﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class TechnicalDocumentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /BackOffice/TechnicalDocument/
        public async Task<ActionResult> Index()
        {
            return View(await db.TechnicalDocuments.ToListAsync());
        }

        // GET: /BackOffice/TechnicalDocument/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            if (technicaldocument == null)
            {
                return HttpNotFound();
            }
            return View(technicaldocument);
        }

        // GET: /BackOffice/TechnicalDocument/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BackOffice/TechnicalDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TechnicalDocument model)
        {
            if (ModelState.IsValid)
            {
                var technicaldocument = new TechnicalDocument
                {
                    Id = model.Id,
                    LastModificationDate = model.LastModificationDate,
                    Title = model.Title,
                    UploadedDate = model.UploadedDate,
                    UriPath = model.UriPath,
                    DocumentType = model.DocumentType,
                    Format = model.Format
                };

                db.TechnicalDocuments.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /BackOffice/TechnicalDocument/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            if (technicaldocument == null)
            {
                return HttpNotFound();
            }
            return View(technicaldocument);
        }

        // POST: /BackOffice/TechnicalDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,UriPath,UpdateDate,LastModificationDate,Format,DocumentType,Language")] TechnicalDocument technicaldocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technicaldocument).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(technicaldocument);
        }

        // GET: /BackOffice/TechnicalDocument/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            if (technicaldocument == null)
            {
                return HttpNotFound();
            }
            return View(technicaldocument);
        }

        // POST: /BackOffice/TechnicalDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            db.TechnicalDocuments.Remove(technicaldocument);
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
