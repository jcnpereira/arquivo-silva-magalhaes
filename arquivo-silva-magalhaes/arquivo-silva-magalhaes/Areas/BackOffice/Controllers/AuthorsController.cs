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
using ArquivoSilvaMagalhaes.Resources;

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
        public async Task<ActionResult> Create(AuthorEditViewModel model)
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

            var authText = author.AuthorTexts.First(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage);

            var model = new AuthorEditViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                LanguageCode = LanguageDefinitions.DefaultLanguage,
                Biography = authText.Biography,
                Curriculum = authText.Curriculum,
                Nationality = authText.Nationality
            };

            return View(model);
        }

        // POST: BackOffice/Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AuthorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = db.AuthorSet.Find(model.Id);

                author.FirstName = model.FirstName;
                author.LastName = model.LastName;
                author.BirthDate = model.BirthDate;
                author.DeathDate = model.DeathDate;

                var text = author.AuthorTexts.First(t => t.LanguageCode == LanguageDefinitions.DefaultLanguage);

                text.Biography = model.Biography;
                text.Curriculum = model.Curriculum;
                text.Nationality = model.Nationality;

                db.Entry(text).State = EntityState.Modified;
                db.Entry(author).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
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

        public async Task<ActionResult> EditText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.MustSpecifyContent);
            }

            AuthorText text = await db.AuthorTextSet.FindAsync(id);

            if (text == null)
            {
                return HttpNotFound();
            }

            return View(new AuthorI18nEditModel
                {
                    Id = text.Id,
                    Biography = text.Biography,
                    Curriculum = text.Curriculum,
                    Nationality = text.Nationality,
                    LanguageCode = text.LanguageCode
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditText(AuthorI18nEditModel textModel)
        {
            if (ModelState.IsValid)
            {
                var text = await db.AuthorTextSet.FindAsync(textModel.Id);

                text.Nationality = textModel.Nationality;
                text.Biography = textModel.Biography;
                text.Curriculum = textModel.Curriculum;

                db.Entry(text).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Details", new { Id = text.Author.Id });
            }

            return View(textModel);
        }


        public async Task<ActionResult> DeleteText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorText text = await db.AuthorTextSet.FindAsync(id);

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
            AuthorText text = await db.AuthorTextSet.FindAsync(id);

            int authId = text.Author.Id;

            // Don't allow removal of texts which are in the default language.
            if (text.LanguageCode == LanguageDefinitions.DefaultLanguage)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.CannotDeleteDefaultLang);
            }

            db.AuthorTextSet.Remove(text);

            await db.SaveChangesAsync();

            return RedirectToAction("Details", new { Id = authId });
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
