using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class EventController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Event
        public async Task<ActionResult> Index()
        {
            return View(await db.Events.ToListAsync());
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

            ViewBag.AreLanguagesMissing = events.EventTexts.Count <= LanguageDefinitions.Languages.Count;

            return View(events);
        }

        // GET: BackOffice/Events/Create
        public ActionResult Create()
        {
            var model = new EventEditViewModel
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            };

            return View(model);
        }

        // POST: BackOffice/Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var events = new Event
                {
                    Id = model.Id,
                    Place = model.Place,
                    Coordinates = model.Coordinates,
                    VisitorInformation = model.VisitorInformation,
                    StartMoment = model.EndMoment,
                    EndMoment = model.EndMoment,
                    PublishDate = model.PublishDate,
                    ExpiryDate = model.ExpiryDate,
                    HideAfterExpiry = model.HideAfterExpiry,
                    EventType = model.EventType
                };

                events.EventTexts.Add(
                new EventTranslation
                {
                    EventId = model.Id,
                    LanguageCode = LanguageDefinitions.DefaultLanguage,
                    Title = model.Title,
                    Heading = model.Heading,
                    SpotLight = model.SpotLight,
                    TextContent = model.TextContent
                });

                db.Events.Add(events);
                await db.SaveChangesAsync();

                if (AreLanguagesMissing(events))
                {
                    // There are languages which may be added.
                    // Ask the used if he/she wants to any.
                    ViewBag.Id = events.Id;
                    return View("_AddLanguagePrompt");
                }

                // We don't need more languages. Redirect to the list.
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// Checks if languages are missing for a given
        /// event.
        /// </summary>
        /// <param name="event">The Event which will be checked</param>
        /// <returns>true if languages are missing, false otherwise.</returns>
        private static bool AreLanguagesMissing(Event events)
        {
            // Check if we need to add more languages.
            var langCodes = events.EventTexts
                                  .Select(t => t.LanguageCode)
                                  .ToList();

            return LanguageDefinitions.Languages.Where(l => !langCodes.Contains(l)).Count() > 0;
        }

        // GET: BackOffice/Event/AddLanguage
        public async Task<ActionResult> AddLanguage(int? Id)
        {
            // There needs to be an event.
            if (Id != null)
            {
                var events = await db.Events.FindAsync(Id);

                if (events == null)
                {
                    return HttpNotFound();
                }

                var langCodes = events.EventTexts
                                      .Select(t => t.LanguageCode)
                                      .ToList();

                // First, we'll check which languages we already have in the DB,
                // we'll remove any which already exist.
                var notDoneLanguages = LanguageDefinitions.Languages
                                                          .Where(l => !langCodes.Contains(l));

                // This should stop naughty attempts at adding a language
                // to an entity which already has all languages done.
                if (notDoneLanguages.Count() == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var model = new EventEditViewModel
                {
                    AvailableLanguages = notDoneLanguages
                                            .Select(l => new SelectListItem
                                            {
                                                // Get the localized language name.
                                                Text = CultureInfo.GetCultureInfo(l).NativeName,
                                                // Text = LanguageDefinitions.GetLanguageNameForCurrentLanguage(l),
                                                Value = l
                                            })
                                            .ToList()
                };

                return View(model);
            }
            else
            {
                // If there's no event, we can't add a language to it.
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddLanguage(EventI18nPartialModels model)
        {
            if (ModelState.IsValid)
            {
                var events = db.Events.Find(model.Id);

                var text = new EventTranslation
                {
                    EventId = model.Id,
                    LanguageCode = model.LanguageCode,
                    Title = model.Title,
                    Heading = model.Heading,
                    SpotLight = model.SpotLight,
                    TextContent = model.TextContent,

                };

                db.EventTranslations.Add(text);
                await db.SaveChangesAsync();

                if (AreLanguagesMissing(events))
                {
                    ViewBag.Id = events.Id;
                    return View("_AddLanguagePrompt");
                }

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

            Event events = await db.Events.FindAsync(id);

            if (events == null)
            {
                return HttpNotFound();
            }

            var eventText = events.EventTexts.First(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage);

            var model = new EventEditViewModel
            {
                Id = events.Id,
                Place = events.Place,
                Coordinates = events.Coordinates,
                VisitorInformation = events.VisitorInformation,
                StartMoment = events.StartMoment,
                EndMoment = events.EndMoment,
                PublishDate = events.PublishDate,
                ExpiryDate = events.ExpiryDate,
                HideAfterExpiry = events.HideAfterExpiry,
                EventType = events.EventType,
                LanguageCode = LanguageDefinitions.DefaultLanguage,
                Title = eventText.Title,
                Heading = eventText.Heading,
                SpotLight = eventText.SpotLight,
                TextContent = eventText.TextContent
            };

            return View(model);
        }

        // POST: BackOffice/Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EventEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                var events = db.Events.Find(model.Id);

                events.Place = model.Place;
                events.Coordinates = model.Coordinates;
                events.StartMoment = model.StartMoment;
                events.EndMoment = model.EndMoment;
                events.PublishDate = model.PublishDate;
                events.ExpiryDate = model.ExpiryDate;
                events.HideAfterExpiry = model.HideAfterExpiry;
                events.EventType = model.EventType;

                var text = events.EventTexts.First(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage);

                text.LanguageCode = model.LanguageCode;
                text.Title = model.Title;
                text.Heading = model.Heading;
                text.SpotLight = model.SpotLight;
                text.TextContent = model.TextContent;

                db.Entry(text).State = EntityState.Modified;
                db.Entry(events).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
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

        public async Task<ActionResult> EditText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.MustSpecifyContent);
            }

            EventTranslation text = await db.EventTranslations.FindAsync(id);

            if (text == null)
            {
                return HttpNotFound();
            }

            return View(new EventI18nPartialModels
            {
                Id = text.EventId,
                LanguageCode = text.LanguageCode,
                Title = text.Title,
                Heading = text.Heading,
                SpotLight = text.SpotLight,
                TextContent = text.TextContent

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditText(EventI18nPartialModels textModel)
        {
            if (ModelState.IsValid)
            {
                var text = await db.EventTranslations.FindAsync(textModel.Id);


                // HACK: For some reason, if I don't specify the event,
                // validation fails.
                text.Event = db.Events.Find(text.Event.Id);
                text.EventId = textModel.Id;
                text.Title = textModel.Title;
                text.Heading = textModel.Heading;
                text.SpotLight = textModel.SpotLight;
                text.TextContent = textModel.TextContent;

                db.Entry(text).State = EntityState.Modified;


                db.SaveChanges();


                return RedirectToAction("Details", new { Id = text.Event.Id });
            }

            return View(textModel);
        }


        public async Task<ActionResult> DeleteText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventTranslation text = await db.EventTranslations.FindAsync(id);

            if (text == null)
            {
                return HttpNotFound();
            }

            // Don't allow removal of texts which are in the default language.
            if (text.LanguageCode == LanguageDefinitions.DefaultLanguage)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.CannotDeleteDefaultLang);
            }

            return View(text);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteText(int id)
        {
            EventTranslation text = await db.EventTranslations.FindAsync(id);

            int eventId = text.Event.Id;

            // Don't allow removal of texts which are in the default language.
            if (text.LanguageCode == LanguageDefinitions.DefaultLanguage)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.CannotDeleteDefaultLang);
            }

            db.EventTranslations.Remove(text);

            await db.SaveChangesAsync();

            return RedirectToAction("Details", new { Id = eventId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditText(EventEditViewModel textModel)
        {
            if (ModelState.IsValid)
            {
                var text = await db.EventTranslations.FindAsync(textModel.Id);


                // HACK: For some reason, if I don't specify the event,
                // validation fails.
                text.Event = db.Events.Find(text.Event.Id);

                text.EventId = textModel.Id;
                text.LanguageCode = textModel.LanguageCode;
                text.Title = textModel.Title;
                text.Heading = textModel.Heading;
                text.SpotLight = textModel.SpotLight;
                text.TextContent = textModel.TextContent;

                db.Entry(text).State = EntityState.Modified;


                db.SaveChanges();


                return RedirectToAction("Details", new { Id = text.Event.Id });
            }

            return View(textModel);
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
