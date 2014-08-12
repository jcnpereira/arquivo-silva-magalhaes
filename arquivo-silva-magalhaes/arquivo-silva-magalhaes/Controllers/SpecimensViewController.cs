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
    public class SpecimensViewController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /SpecimensView/
        public async Task<ActionResult> Index()
        {
            return View(await db.ProcessViewModels.ToListAsync());
        }

        // GET: /SpecimensView/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessViewModel processviewmodel = await db.ProcessViewModels.FindAsync(id);
            if (processviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(processviewmodel);
        }

        // GET: /SpecimensView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SpecimensView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Value")] ProcessViewModel processviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.ProcessViewModels.Add(processviewmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(processviewmodel);
        }

        // GET: /SpecimensView/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessViewModel processviewmodel = await db.ProcessViewModels.FindAsync(id);
            if (processviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(processviewmodel);
        }

        // POST: /SpecimensView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Value")] ProcessViewModel processviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(processviewmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(processviewmodel);
        }

        // GET: /SpecimensView/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessViewModel processviewmodel = await db.ProcessViewModels.FindAsync(id);
            if (processviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(processviewmodel);
        }

        // POST: /SpecimensView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProcessViewModel processviewmodel = await db.ProcessViewModels.FindAsync(id);
            db.ProcessViewModels.Remove(processviewmodel);
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
