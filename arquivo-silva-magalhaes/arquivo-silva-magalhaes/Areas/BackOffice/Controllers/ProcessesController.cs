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
    public class ProcessesController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Processes
        public async Task<ActionResult> Index()
        {
            return View(await db.ProcessSet.ToListAsync());
        }

        // GET: BackOffice/Processes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await db.ProcessSet.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // GET: BackOffice/Processes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Processes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id")] Process process)
        {
            if (ModelState.IsValid)
            {
                db.ProcessSet.Add(process);
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
            Process process = await db.ProcessSet.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // POST: BackOffice/Processes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] Process process)
        {
            if (ModelState.IsValid)
            {
                db.Entry(process).State = EntityState.Modified;
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
            Process process = await db.ProcessSet.FindAsync(id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        // POST: BackOffice/Processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Process process = await db.ProcessSet.FindAsync(id);
            db.ProcessSet.Remove(process);
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
