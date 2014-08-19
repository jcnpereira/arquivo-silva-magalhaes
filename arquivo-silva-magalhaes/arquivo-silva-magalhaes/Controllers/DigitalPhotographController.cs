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
    public class DigitalPhotographController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();



        public ActionResult SetLanguage(string lang, string returnUrl)
        {
            Response.SetCookie(new HttpCookie("lang", lang));

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        // GET: /DigitalPhotograph/
        public async Task<ActionResult> Index()
        {
            var digitalphotographs = db.DigitalPhotographs.Include(d => d.Specimen);
            return View(await digitalphotographs.ToListAsync());
        }

        // GET: /DigitalPhotograph/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalphotograph = await db.DigitalPhotographs.FindAsync(id);
            if (digitalphotograph == null)
            {
                return HttpNotFound();
            }
            return View(digitalphotograph);
        }

        // GET: /DigitalPhotograph/Create
        public ActionResult Create()
        {
            ViewBag.SpecimenId = new SelectList(db.Specimens, "Id", "AuthorCatalogationCode");
            return View();
        }

        // POST: /DigitalPhotograph/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,SpecimenId,ScanDate,FileName,OriginalFileName,MimeType,LastModified,Process,CopyrightInfo,IsVisible")] DigitalPhotograph digitalphotograph)
        {
            if (ModelState.IsValid)
            {
                db.DigitalPhotographs.Add(digitalphotograph);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SpecimenId = new SelectList(db.Specimens, "Id", "AuthorCatalogationCode", digitalphotograph.SpecimenId);
            return View(digitalphotograph);
        }

        // GET: /DigitalPhotograph/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalphotograph = await db.DigitalPhotographs.FindAsync(id);
            if (digitalphotograph == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecimenId = new SelectList(db.Specimens, "Id", "AuthorCatalogationCode", digitalphotograph.SpecimenId);
            return View(digitalphotograph);
        }

        // POST: /DigitalPhotograph/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,SpecimenId,ScanDate,FileName,OriginalFileName,MimeType,LastModified,Process,CopyrightInfo,IsVisible")] DigitalPhotograph digitalphotograph)
        {
            if (ModelState.IsValid)
            {
                db.Entry(digitalphotograph).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SpecimenId = new SelectList(db.Specimens, "Id", "AuthorCatalogationCode", digitalphotograph.SpecimenId);
            return View(digitalphotograph);
        }

        // GET: /DigitalPhotograph/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalphotograph = await db.DigitalPhotographs.FindAsync(id);
            if (digitalphotograph == null)
            {
                return HttpNotFound();
            }
            return View(digitalphotograph);
        }

        // POST: /DigitalPhotograph/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DigitalPhotograph digitalphotograph = await db.DigitalPhotographs.FindAsync(id);
            db.DigitalPhotographs.Remove(digitalphotograph);
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
