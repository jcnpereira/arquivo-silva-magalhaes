using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ShowCasePhotoesController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /ShowCasePhotoes/
        public async Task<ActionResult> Index()
        {
            var showcasephotoes = db.ShowcasePhotoes.Include(s => s.Image);
            return View(await showcasephotoes.ToListAsync());
        }

        // GET: /ShowCasePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasephoto = await db.ShowcasePhotoes.FindAsync(id);
            if (showcasephoto == null)
            {
                return HttpNotFound();
            }
            return View(showcasephoto);
        }

        // GET: /ShowCasePhotoes/Create
        public ActionResult Create()
        {
            ViewBag.DigitalPhotographId = new SelectList(db.DigitalPhotographs, "Id", "FileName");
            return View();
        }

        // POST: /ShowCasePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,CommenterName,CommenterEmail,IsEmailVisible,VisibleSince,DigitalPhotographId")] ShowcasePhoto showcasephoto)
        {
            if (ModelState.IsValid)
            {
                db.ShowcasePhotoes.Add(showcasephoto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DigitalPhotographId = new SelectList(db.DigitalPhotographs, "Id", "FileName", showcasephoto.ImageId);
            return View(showcasephoto);
        }

        // GET: /ShowCasePhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasephoto = await db.ShowcasePhotoes.FindAsync(id);
            if (showcasephoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.DigitalPhotographId = new SelectList(db.DigitalPhotographs, "Id", "FileName", showcasephoto.ImageId);
            return View(showcasephoto);
        }

        // POST: /ShowCasePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,CommenterName,CommenterEmail,IsEmailVisible,VisibleSince,DigitalPhotographId")] ShowcasePhoto showcasephoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showcasephoto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DigitalPhotographId = new SelectList(db.DigitalPhotographs, "Id", "FileName", showcasephoto.ImageId);
            return View(showcasephoto);
        }

        // GET: /ShowCasePhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasephoto = await db.ShowcasePhotoes.FindAsync(id);
            if (showcasephoto == null)
            {
                return HttpNotFound();
            }
            return View(showcasephoto);
        }

        // POST: /ShowCasePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShowcasePhoto showcasephoto = await db.ShowcasePhotoes.FindAsync(id);
            db.ShowcasePhotoes.Remove(showcasephoto);
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
