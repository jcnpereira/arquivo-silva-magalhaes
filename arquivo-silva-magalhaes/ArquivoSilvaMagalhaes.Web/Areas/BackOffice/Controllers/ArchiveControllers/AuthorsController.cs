using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class AuthorsController : ArchiveControllerBase
    {
        private ITranslateableRepository<Author, AuthorTranslation> db;

        public AuthorsController()
            : this(new TranslateableRepository<Author, AuthorTranslation>()) { }

        public AuthorsController(ITranslateableRepository<Author, AuthorTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Authors
        public async Task<ActionResult> Index(int pageNumber = 1, string query = null)
        {
            var model = (await db.Entities
                .Where(a => query == null || a.FirstName.Contains(query) || a.LastName.Contains(query))
                .OrderBy(a => a.Id)
                .ToListAsync())
                .Select(a => new TranslatedViewModel<Author, AuthorTranslation>(a))
                .ToPagedList(pageNumber, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }


            return View(model);
        }

        // GET: BackOffice/Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = await db.GetByIdAsync(id);

            if (author == null)
            {
                return HttpNotFound();
            }

            author.Translations = author.Translations.ToList();
            author.Documents = author.Documents.ToList();

            return View(author);
        }

        // GET: BackOffice/Authors/Create
        public ActionResult Create()
        {
            var author = new Author();

            author.Translations.Add(new AuthorTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

            var model = new AuthorEditViewModel
            {
                Author = author
            };

            return View(model);
        }

        // POST: BackOffice/Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Add(author);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(new AuthorEditViewModel { Author = author });
        }

        // GET: BackOffice/Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Author author = await db.GetByIdAsync(id);

            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(new AuthorEditViewModel
                {
                    Author = author
                });
        }

        // POST: BackOffice/Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Update(author);

                // Update each translation.
                foreach (var t in author.Translations)
                {
                    db.UpdateTranslation(t);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(new AuthorEditViewModel
                {
                    Author = author
                });
        }

        // GET: BackOffice/Authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = await db.GetByIdAsync(id);

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
            await db.RemoveByIdAsync(id);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #region Translation Actions

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = await db.GetByIdAsync(id);

            if (author == null)
            {
                return HttpNotFound();
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                author.Translations.Select(t => t.LanguageCode).ToArray());

            return View(new AuthorTranslation
                {
                    AuthorId = author.Id
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(AuthorTranslation translation)
        {
            var author = await db.GetByIdAsync(translation.AuthorId);

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
            if (id == null || String.IsNullOrEmpty(languageCode) || languageCode == LanguageDefinitions.DefaultLanguage)
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
            if (id == null || String.IsNullOrEmpty(languageCode))
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

        #endregion Translation Actions

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