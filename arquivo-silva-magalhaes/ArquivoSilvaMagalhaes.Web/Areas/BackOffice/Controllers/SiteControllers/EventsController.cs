using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class EventsController : SiteControllerBase
    {
        private ITranslateableRepository<Event, EventTranslation> db;

        public EventsController()
            : this(new TranslateableRepository<Event, EventTranslation>())
        {
        }

        public EventsController(ITranslateableRepository<Event, EventTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Event
        public async Task<ActionResult> Index(int pageNumber = 1, string query = "")
        {
            var model = (await db.Entities
                .Where(e => e.Translations.Any(t => t.Title.Contains(query)))
                .OrderBy(b => b.Id)
                .ToListAsync())
                .Select(e => new TranslatedViewModel<Event, EventTranslation>(e))
                .ToPagedList(pageNumber, 10);

            ViewBag.Query = query;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        // GET: BackOffice/Event/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event e = await db.GetByIdAsync(id);

            if (e == null)
            {
                return HttpNotFound();
            }

            e.Translations = e.Translations.ToList();

            return View(e);
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
        public async Task<ActionResult> Create(EventEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Attachments != null)
                {
                    foreach (var attachment in model.Attachments)
                    {
                        var attachedFile = model.Files.FirstOrDefault(f => f.FileName == attachment.FileName);

                        if (attachedFile != null)
                        {
                            var newName = Guid.NewGuid().ToString() + "_" + Path.GetExtension(attachedFile.FileName);
                            var dir = "~/Public/Attachments/";

                            Directory.CreateDirectory(Server.MapPath(dir));

                            attachedFile.SaveAs(Server.MapPath(dir + newName));

                            model.Event.Attachments.Add(new Attachment
                                {
                                    FileName = newName,
                                    MimeFormat = attachedFile.ContentType,
                                    Size = attachedFile.ContentLength,
                                    Title = attachment.Title
                                });
                        }
                    }
                }

                db.Add(model.Event);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BackOffice/Events/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = await db.GetByIdAsync(id);

            if (e == null)
            {
                return HttpNotFound();
            }

            return View(GenerateViewModel(e));
        }

        // POST: BackOffice/Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Update(@event);

                foreach (var t in @event.Translations)
                {
                    db.UpdateTranslation(t);
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
            Event events = await db.GetByIdAsync(id);
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
            var e = await db.GetByIdAsync(id);

            foreach (var att in e.Attachments.ToList())
            {
                var path = Server.MapPath("~/Public/Attachments/" + att.FileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            db.Remove(e);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private EventEditViewModel GenerateViewModel(Event e)
        {
            var model = new EventEditViewModel();
            model.Event = e;

            return model;
        }

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var e = await db.GetByIdAsync(id);

            if (e == null)
            {
                return HttpNotFound();
            }

            if (e.Translations.Count == LanguageDefinitions.Languages.Count)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                e.Translations.Select(t => t.LanguageCode).ToArray());

            return View(new EventTranslation
                {
                    EventId = e.Id
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(EventTranslation translation)
        {
            var author = await db.GetByIdAsync(translation.EventId);

            if (ModelState.IsValid)
            {
                author.Translations.Add(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                author.Translations.Select(t => t.LanguageCode).ToArray());

            return View(translation);
        }

        public async Task<ActionResult> DeleteTranslation(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode) || languageCode == LanguageDefinitions.DefaultLanguage)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            return View(tr);
        }

        [HttpPost]
        [ActionName("DeleteTranslation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTranslationConfirmed(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            await db.RemoveTranslationByIdAsync(id, languageCode);

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}