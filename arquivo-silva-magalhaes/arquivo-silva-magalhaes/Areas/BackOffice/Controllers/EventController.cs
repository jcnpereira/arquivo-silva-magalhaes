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
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class EventController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/Event/
        public async Task<ActionResult> Index()
        {
            return View(await db.EventSet.ToListAsync());
        }

        // GET: /BackOffice/Event/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.EventSet.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: /BackOffice/Event/Create
        public ActionResult Create()
        {

            var model = new EventEditViewModel
            {
                //EventTypes = new List<SelectListItem>
                //{
                //    new SelectListItem { Value = EventType.Expo + "", Text = "Exposição" },
                //    new SelectListItem { Value = EventType.School + "", Text = "Educação"},
                //    new SelectListItem { Value = EventType.Other + "", Text = "Outro"}
                //}
            };

            return View(model);
        }

        // POST: /BackOffice/Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventText = new EventText
                {
               //     LanguageCode = model.LanguageCode
                };

                //db.Events.Add(@event);
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /BackOffice/Event/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.EventSet.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include="Id,Place,Coordinates,VisitorInformation,StartMoment,EndMoment,PublishDate,ExpiryDate,HideAfterExpiry,EventType")] Event @event)
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
            Event @event = await db.EventSet.FindAsync(id);
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
            Event @event = await db.EventSet.FindAsync(id);
            db.EventSet.Remove(@event);
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
