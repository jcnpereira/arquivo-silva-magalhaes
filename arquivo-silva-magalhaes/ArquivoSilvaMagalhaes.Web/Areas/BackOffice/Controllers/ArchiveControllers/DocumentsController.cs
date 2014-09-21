using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
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
    public class DocumentsController : ArchiveControllerBase
    {
        private ITranslateableRepository<Document, DocumentTranslation> db;

        public DocumentsController()
            : this(new TranslateableRepository<Document, DocumentTranslation>()) { }

        public DocumentsController(TranslateableRepository<Document, DocumentTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Documents
        public async Task<ActionResult> Index(int pageNumber = 1, int authorId = 0, int collectionId = 0)
        {
            var query = await db.Entities
                                .Where(d => (authorId == 0 || d.AuthorId == authorId) &&
                                            (collectionId == 0 || d.CollectionId == collectionId))
                                .ToListAsync();

            return View(query.Select(d => new TranslatedViewModel<Document, DocumentTranslation>(d))
                             .ToPagedList(pageNumber, 10));
        }

        // GET: BackOffice/Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = await db.GetByIdAsync(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            document.Translations = document.Translations.ToList();

            return View(document);
        }

        // GET: BackOffice/Documents/Create
        public async Task<ActionResult> Create(int? collectionId, int? authorId)
        {
            var doc = new Document();

            doc.Translations.Add(new DocumentTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            // Check for a collection.
            if (collectionId != null && db.Set<Collection>().Any(c => c.Id == collectionId))
            {
                doc.CatalogCode = CodeGenerator.SuggestDocumentCode(collectionId.Value);
                doc.CollectionId = collectionId.Value;
            }

            // Check for an author.
            if (authorId != null && db.Set<Author>().Any(a => a.Id == authorId))
            {
                doc.AuthorId = authorId.Value;
            }

            var model = new DocumentEditViewModel(doc);
            await model.PopulateDropDownLists(db.Set<Author>(), db.Set<Collection>());

            return View(model);
        }

        // POST: BackOffice/Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Add(document);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var model = new DocumentEditViewModel(document);
            await model.PopulateDropDownLists(db.Set<Author>(), db.Set<Collection>());

            return View(model);
        }

        // GET: BackOffice/Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = await db.GetByIdAsync(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            document.Translations = document.Translations.ToList();

            var model = new DocumentEditViewModel(document);
            await model.PopulateDropDownLists(db.Set<Author>(), db.Set<Collection>());

            return View(model);
        }

        // POST: BackOffice/Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                foreach (var t in document.Translations)
                {
                    db.UpdateTranslation(t);
                }

                db.Update(document);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var model = new DocumentEditViewModel(document);
            await model.PopulateDropDownLists(db.Set<Author>(), db.Set<Collection>());

            return View(model);
        }

        // GET: BackOffice/Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.GetByIdAsync(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            document.Translations = document.Translations.ToList();

            return View(document);
        }

        // POST: BackOffice/Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.RemoveByIdAsync(id);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doc = await db.GetByIdAsync(id);

            if (doc == null)
            {
                return HttpNotFound();
            }

            var translations = doc.Translations
                                  .Select(dt => dt.LanguageCode)
                                  .ToList();

            if (translations.Count() == LanguageDefinitions.Languages.Count)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new DocumentTranslation
            {
                DocumentId = doc.Id
            };

            ViewBag.Languages =
                LanguageDefinitions.GenerateAvailableLanguageDDL(doc.Translations.Select(t => t.LanguageCode).ToList());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(DocumentTranslation translation)
        {
            if (ModelState.IsValid)
            {
                db.AddTranslation(translation);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

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

        public ActionResult SuggestCode(int? collectionId)
        {
            if (collectionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var suggestedCode = CodeGenerator.SuggestDocumentCode(collectionId.Value);

            return Json(suggestedCode, JsonRequestBehavior.AllowGet);
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