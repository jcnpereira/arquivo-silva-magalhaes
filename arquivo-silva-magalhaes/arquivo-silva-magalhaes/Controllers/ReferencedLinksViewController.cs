using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Models;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ReferencedLinksViewController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /ReferencedLinksView/
        public async Task<ActionResult> Index()
        {
            return View(await db.ReferencedLinkModels.ToListAsync());
        }

        // GET: /ReferencedLinksView/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLinkModels referencedlinkmodels = await db.ReferencedLinkModels.FindAsync(id);
            if (referencedlinkmodels == null)
            {
                return HttpNotFound();
            }
            return View(referencedlinkmodels);
        }

        // GET: /ReferencedLinksView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ReferencedLinksView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Title,Link,Description,DateOfCreation,LastModifiedDate,IsUsefulLink")] ReferencedLinkModels referencedlinkmodels)
        {
            if (ModelState.IsValid)
            {
                db.ReferencedLinkModels.Add(referencedlinkmodels);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(referencedlinkmodels);
        }

        // GET: /ReferencedLinksView/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLinkModels referencedlinkmodels = await db.ReferencedLinkModels.FindAsync(id);
            if (referencedlinkmodels == null)
            {
                return HttpNotFound();
            }
            return View(referencedlinkmodels);
        }

        // POST: /ReferencedLinksView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Title,Link,Description,DateOfCreation,LastModifiedDate,IsUsefulLink")] ReferencedLinkModels referencedlinkmodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referencedlinkmodels).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(referencedlinkmodels);
        }

        // GET: /ReferencedLinksView/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLinkModels referencedlinkmodels = await db.ReferencedLinkModels.FindAsync(id);
            if (referencedlinkmodels == null)
            {
                return HttpNotFound();
            }
            return View(referencedlinkmodels);
        }

        // POST: /ReferencedLinksView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReferencedLinkModels referencedlinkmodels = await db.ReferencedLinkModels.FindAsync(id);
            db.ReferencedLinkModels.Remove(referencedlinkmodels);
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
