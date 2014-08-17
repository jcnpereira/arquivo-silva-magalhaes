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
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class CollectionsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        //public ActionResult Indexx()
        //{
        //    Collection collection = new Collection();
        //    List<CollectionTranslation> translations = collection.Translations.ToList();
        //    return View(translations);
        //}

        // GET: Collections
        public async Task<ActionResult> Index()
        {

            return View(await db.Collections.ToListAsync());
        }


        // GET: Collections/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        //    // GET: Collections/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Collections/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<ActionResult> Create([Bind(Include = "Id,Type,InitialProductionDate,EndProductionDate,LogoLocation,AttachmentsDescriptions,OrganizationSystem,Notes,IsVisible,CatalogCode")] Collection collection)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Collections.Add(collection);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }

        //        return View(collection);
        //    }

        //    // GET: Collections/Edit/5
        //    public async Task<ActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Collection collection = await db.Collections.FindAsync(id);
        //        if (collection == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(collection);
        //    }

        //    // POST: Collections/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<ActionResult> Edit([Bind(Include = "Id,Type,InitialProductionDate,EndProductionDate,LogoLocation,AttachmentsDescriptions,OrganizationSystem,Notes,IsVisible,CatalogCode")] Collection collection)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(collection).State = EntityState.Modified;
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //        return View(collection);
        //    }

        //    // GET: Collections/Delete/5
        //    public async Task<ActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Collection collection = await db.Collections.FindAsync(id);
        //        if (collection == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(collection);
        //    }

        //    // POST: Collections/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<ActionResult> DeleteConfirmed(int id)
        //    {
        //        Collection collection = await db.Collections.FindAsync(id);
        //        db.Collections.Remove(collection);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
    }
}
