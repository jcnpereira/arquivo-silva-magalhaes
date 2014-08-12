using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Models;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class KeywordViewController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /KeywordView/
        public async Task<ActionResult> Index()
        {
            return View(await db.KeywordEditViewModels.ToListAsync());
        }

        // GET: /KeywordView/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeywordEditViewModel keywordeditviewmodel = await db.KeywordEditViewModels.FindAsync(id);
            if (keywordeditviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(keywordeditviewmodel);
        }

        // GET: /KeywordView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /KeywordView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,KeywordId,LanguageCode,Value")] KeywordEditViewModel keywordeditviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.KeywordEditViewModels.Add(keywordeditviewmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(keywordeditviewmodel);
        }

        // GET: /KeywordView/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeywordEditViewModel keywordeditviewmodel = await db.KeywordEditViewModels.FindAsync(id);
            if (keywordeditviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(keywordeditviewmodel);
        }

        // POST: /KeywordView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,KeywordId,LanguageCode,Value")] KeywordEditViewModel keywordeditviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keywordeditviewmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(keywordeditviewmodel);
        }

        // GET: /KeywordView/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KeywordEditViewModel keywordeditviewmodel = await db.KeywordEditViewModels.FindAsync(id);
            if (keywordeditviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(keywordeditviewmodel);
        }

        // POST: /KeywordView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            KeywordEditViewModel keywordeditviewmodel = await db.KeywordEditViewModels.FindAsync(id);
            db.KeywordEditViewModels.Remove(keywordeditviewmodel);
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
