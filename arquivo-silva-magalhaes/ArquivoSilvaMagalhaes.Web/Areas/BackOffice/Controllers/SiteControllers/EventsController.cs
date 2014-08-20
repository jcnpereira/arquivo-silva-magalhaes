using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class EventsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Event
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => db.EventTranslations
                .Include(et => et.Event)
                .Where(et => et.LanguageCode == LanguageDefinitions.DefaultLanguage)
                .OrderBy(et => et.EventId)
                .ToPagedList(pageNumber, 10)));
        }

        // GET: BackOffice/Event/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event events = await db.Events.FindAsync(id);
            if (events == null)
            {
                return HttpNotFound();
            }

            ViewBag.AreLanguagesMissing = events.Translations.Count <= LanguageDefinitions.Languages.Count;

            return View(events);
        }

        // GET: BackOffice/Events/Create
        public ActionResult Create()
        {
            var e = new Event();
            e.Translations.Add(new EventTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

            return View(GenerateViewModel(e));
        }

        // POST: BackOffice/Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Event @event, int[] eventIds, int[] linkIds)
        {
            if (ModelState.IsValid)
            {
                if (linkIds != null)
                {
                    @event.Links = await db.ReferencedLinks
                        .Where(rl => linkIds.Contains(rl.Id)).ToListAsync();
                }

                db.Events.Add(@event);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(@event));
        }

        // GET: BackOffice/Events/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = await db.Events.FindAsync(id);

            if (e == null)
            {
                return HttpNotFound();
            }

            return View(GenerateViewModel(e));
        }

        // POST: BackOffice/Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Event @event, int[] eventIds, int[] linkIds)
        {
            if (ModelState.IsValid)
            {
                // "Force-load" the collection and the authors.
                if (eventIds != null || linkIds != null)
                {
                    db.Events.Attach(@event);

                    if (linkIds != null)
                    {
                        db.Entry(@event).Collection(ev => ev.Links).Load();
                        // The forced-loading was required so that the author list can be updated.
                        @event.Links = db.ReferencedLinks.Where(rl => eventIds.Contains(rl.Id)).ToList();
                    }
                }

                db.Entry(@event).State = EntityState.Modified;

                foreach (var t in @event.Translations)
                {
                    db.Entry(t).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(@event));
            
        }

        // GET: BackOffice/Event/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event events = await db.Events.FindAsync(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: BackOffice/Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event events = await db.Events.FindAsync(id);
            db.Events.Remove(events);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        private EventEditViewModel GenerateViewModel(Event e)
        {
            
                var model = new EventEditViewModel();
                model.Event = e;

                return model;
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
