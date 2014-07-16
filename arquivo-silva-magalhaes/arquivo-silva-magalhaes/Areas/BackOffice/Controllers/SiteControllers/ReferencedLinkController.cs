using System;
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

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ReferencedLinkController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/ReferencedLink/
        public async Task<ActionResult> Index()
        {
            return View(await db.ReferencedLinks.ToListAsync());
        }

        // GET: /BackOffice/ReferencedLink/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }
            return View(referencedlink);
        }

        // GET: /BackOffice/ReferencedLink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BackOffice/ReferencedLink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Title,Link,Description,DateOfCreation,LastModifiedDate,IsUsefulLink")] ReferencedLink referencedlink)
        {

            //var link = new ReferencedLink
            //{
            //    Id=referencedlink.Id,
            //    Title=referencedlink.Title,
            //    Link=referencedlink.Link,
            //    DateOfCreation=DateTime.Now,
            //    LastModifiedDate = DateTime.Now,
            //    IsUsefulLink=referencedlink.IsUsefulLink
            //};
            if (ModelState.IsValid)
            {
                db.ReferencedLinks.Add(referencedlink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(referencedlink);
        }

        // GET: /BackOffice/ReferencedLink/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }
            return View(referencedlink);
        }

        // POST: /BackOffice/ReferencedLink/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Title,Link,Description,DateOfCreation,LastModifiedDate,IsUsefulLink")] ReferencedLink referencedlink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referencedlink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(referencedlink);
        }

        // GET: /BackOffice/ReferencedLink/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }
            return View(referencedlink);
        }

        // POST: /BackOffice/ReferencedLink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            db.ReferencedLinks.Remove(referencedlink);
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
