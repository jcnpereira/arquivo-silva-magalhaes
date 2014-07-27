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

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ShowcasePhotoesController : BackOfficeController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/ShowcasePhotoes
        public async Task<ActionResult> Index()
        {
            var showcasePhotoSet = _db.ShowcasePhotoes.Include(s => s.DigitalPhotograph);
            return View(await showcasePhotoSet.ToListAsync());
        }

        // GET: BackOffice/ShowcasePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            return View(showcasePhoto);
        }

        // GET: BackOffice/ShowcasePhotoes/Create
        public ActionResult Create()
        {
            ViewBag.DigitalPhotographId = new SelectList(_db.DigitalPhotographs, "Id", "ScanDate");
            return View();
        }

        // POST: BackOffice/ShowcasePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CommenterName,CommenterEmail,IsEmailVisible,VisibleSince,DigitalPhotographId")] ShowcasePhoto showcasePhoto)
        {
            if (ModelState.IsValid)
            {
                _db.ShowcasePhotoes.Add(showcasePhoto);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DigitalPhotographId = new SelectList(_db.DigitalPhotographs, "Id", "ScanDate", showcasePhoto.DigitalPhotographId);
            return View(showcasePhoto);
        }

        // GET: BackOffice/ShowcasePhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.DigitalPhotographId = new SelectList(_db.DigitalPhotographs, "Id", "ScanDate", showcasePhoto.DigitalPhotographId);
            return View(showcasePhoto);
        }

        // POST: BackOffice/ShowcasePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CommenterName,CommenterEmail,IsEmailVisible,VisibleSince,DigitalPhotographId")] ShowcasePhoto showcasePhoto)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(showcasePhoto).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DigitalPhotographId = new SelectList(_db.DigitalPhotographs, "Id", "ScanDate", showcasePhoto.DigitalPhotographId);
            return View(showcasePhoto);
        }

        // GET: BackOffice/ShowcasePhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            return View(showcasePhoto);
        }

        // POST: BackOffice/ShowcasePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            _db.ShowcasePhotoes.Remove(showcasePhoto);
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
