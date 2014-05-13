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
    public class DigitalPhotographsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/DigitalPhotographs
        public async Task<ActionResult> Index()
        {
            // var digitalPhotographSet = db.DigitalPhotographSet.Include(d => d.Specimen);
            // return View(await digitalPhotographSet.ToListAsync());
            return View();
        }

        // GET: BackOffice/DigitalPhotographs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalPhotograph = await db.DigitalPhotographSet.FindAsync(id);
            if (digitalPhotograph == null)
            {
                return HttpNotFound();
            }
            return View(digitalPhotograph);
        }

        // GET: BackOffice/DigitalPhotographs/Create
        public ActionResult Create()
        {
            ViewBag.SpecimenId = new SelectList(db.SpecimenSet, "Id", "CatalogCode");
            return View();
        }

        // POST: BackOffice/DigitalPhotographs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ScanDate,StoreLocation,Process,CopyrightInfo,IsVisible,SpecimenId")] DigitalPhotograph digitalPhotograph)
        {
            if (ModelState.IsValid)
            {
                db.DigitalPhotographSet.Add(digitalPhotograph);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SpecimenId = new SelectList(db.SpecimenSet, "Id", "CatalogCode", digitalPhotograph.SpecimenId);
            return View(digitalPhotograph);
        }

        // GET: BackOffice/DigitalPhotographs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalPhotograph = await db.DigitalPhotographSet.FindAsync(id);
            if (digitalPhotograph == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecimenId = new SelectList(db.SpecimenSet, "Id", "CatalogCode", digitalPhotograph.SpecimenId);
            return View(digitalPhotograph);
        }

        // POST: BackOffice/DigitalPhotographs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ScanDate,StoreLocation,Process,CopyrightInfo,IsVisible,SpecimenId")] DigitalPhotograph digitalPhotograph)
        {
            if (ModelState.IsValid)
            {
                db.Entry(digitalPhotograph).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SpecimenId = new SelectList(db.SpecimenSet, "Id", "CatalogCode", digitalPhotograph.SpecimenId);
            return View(digitalPhotograph);
        }

        // GET: BackOffice/DigitalPhotographs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalPhotograph = await db.DigitalPhotographSet.FindAsync(id);
            if (digitalPhotograph == null)
            {
                return HttpNotFound();
            }
            return View(digitalPhotograph);
        }

        // POST: BackOffice/DigitalPhotographs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DigitalPhotograph digitalPhotograph = await db.DigitalPhotographSet.FindAsync(id);
            db.DigitalPhotographSet.Remove(digitalPhotograph);
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
