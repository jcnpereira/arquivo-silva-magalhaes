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
    public class EventController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/Event/
        public async Task<ActionResult> Index()
        {
            return View(await db.Events.ToListAsync());
        }

        // GET: /BackOffice/Event/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: /BackOffice/Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BackOffice/Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Title,Heading,TextContent,Place,Coordinates,VisitorInformation,StartMoment,EndMoment,PublishDate,ExpiryDate,HideAfterExpiry,EventType")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: /BackOffice/Event/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: /BackOffice/Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Title,Heading,TextContent,Place,Coordinates,VisitorInformation,StartMoment,EndMoment,PublishDate,ExpiryDate,HideAfterExpiry,EventType")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: /BackOffice/Event/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: /BackOffice/Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            db.Events.Remove(@event);
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
