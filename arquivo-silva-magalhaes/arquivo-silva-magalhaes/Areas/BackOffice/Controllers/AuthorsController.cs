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
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Utilitites;
using System.Globalization;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class AuthorsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Authors
        public async Task<ActionResult> Index()
        {
            return View(await db.AuthorSet.ToListAsync());
        }

        // GET: BackOffice/Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.AuthorSet.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: BackOffice/Authors/Create
        public ActionResult Create()
        {
            var model = new AuthorEditViewModel
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            };

            return View(model);
        }

        // POST: BackOffice/Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "Id")] AuthorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    DeathDate = model.DeathDate
                };

                author.AuthorTexts.Add(
                new AuthorText
                {
                    Author = author,
                    Biography = model.Biography,
                    Curriculum = model.Curriculum,
                    Nationality = model.Nationality,

                    // On the creation, we'll assume the default language,
                    // this prevents overposting attacks.
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

                db.AuthorSet.Add(author);

                await db.SaveChangesAsync();

                if (AreLanguagesMissing(author))
                {
                    // There are languages which may be added.
                    // Ask the used if he/she wants to any.
                    ViewBag.Id = author.Id;
                    return View("_AddLanguagePrompt");
                }

                // We don't need more languages. Redirect to the list.
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// Checks if languages are missing for a given
        /// author.
        /// </summary>
        /// <param name="author">The Author which will be checked</param>
        /// <returns>true if languages are missing, false otherwise.</returns>
        private static bool AreLanguagesMissing(Author author)
        {
            // Check if we need to add more languages.
            var langCodes = author.AuthorTexts
                                  .Select(t => t.LanguageCode)
                                  .ToList();

            return LanguageDefinitions.Languages.Where(l => !langCodes.Contains(l)).Count() > 0;
        }

        // GET: BackOffice/Authors/AddLanguage
        public async Task<ActionResult> AddLanguage(int? Id)
        {
            // There needs to be an author.
            if (Id != null)
            {
                var author = await db.AuthorSet.FindAsync(Id);

                if (author == null)
                {
                    return HttpNotFound();
                }

                var langCodes = author.AuthorTexts
                                      .Select(t => t.LanguageCode)
                                      .ToList();

                // First, we'll check which languages we already have in the DB,
                // we'll remove any which already exist.
                var notDoneLanguages = LanguageDefinitions.Languages
                                                          .Where(l => !langCodes.Contains(l));

                // This should stop naughty attempts at adding a language
                // to an entity which already has all languages done.
                if (notDoneLanguages.Count() == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                // Populate a SelectList with all available languages.
                ViewBag.AvailableLanguages = notDoneLanguages
                                                .Select(l => new SelectListItem
                                                {
                                                    // Get the localized language name.
                                                    Text = CultureInfo.GetCultureInfo(l).NativeName,
                                                    // Text = LanguageDefinitions.GetLanguageNameForCurrentLanguage(l),
                                                    Value = l
                                                })
                                                .ToList();

                return View();
            }
            else
            {
                // If there's no author, we can't add a language to it.
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddLanguage(AuthorI18nEditModel model)
        {
            if (ModelState.IsValid)
            {
                var author = db.AuthorSet.Find(model.Id);

                var text = new AuthorText
                {
                    Author = db.AuthorSet.Find(model.Id),
                    LanguageCode = model.LanguageCode,
                    Nationality = model.Nationality,
                    Biography = model.Biography,
                    Curriculum = model.Curriculum
                };

                db.AuthorTextSet.Add(text);
                await db.SaveChangesAsync();

                var langCodes = author.AuthorTexts
                                      .Select(t => t.LanguageCode)
                                      .ToList();

                // First, we'll check which languages we already have in the DB,
                // we'll remove any which already exist.
                var notDoneLanguages = LanguageDefinitions.Languages
                                                          .Where(l => !langCodes.Contains(l));
                if (notDoneLanguages.Count() > 0)
                {
                    ViewBag.Id = model.Id;

                    return View("_AddLanguagePrompt");

                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BackOffice/Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.AuthorSet.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: BackOffice/Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,BirthDate,DeathDate")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: BackOffice/Authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.AuthorSet.FindAsync(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: BackOffice/Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Author author = await db.AuthorSet.FindAsync(id);
            db.AuthorSet.Remove(author);
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
