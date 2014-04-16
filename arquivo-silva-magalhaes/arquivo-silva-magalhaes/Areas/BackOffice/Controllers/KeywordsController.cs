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
    public class KeywordsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Keywords
        public async Task<ActionResult> Index()
        {
            return View(await db.KeywordSet.ToListAsync());
        }

        // GET: BackOffice/Keywords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.KeywordSet.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // GET: BackOffice/Keywords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Keywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.KeywordSet.Add(keyword);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(keyword);
        }

        // GET: BackOffice/Keywords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.KeywordSet.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: BackOffice/Keywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyword).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: BackOffice/Keywords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.KeywordSet.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: BackOffice/Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Keyword keyword = await db.KeywordSet.FindAsync(id);
            db.KeywordSet.Remove(keyword);
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
