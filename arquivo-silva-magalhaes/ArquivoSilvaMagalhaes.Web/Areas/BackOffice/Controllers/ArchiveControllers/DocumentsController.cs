using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class DocumentsController : ArchiveControllerBase
    {
        private ITranslateableEntityRepository<Document, DocumentTranslation> _db;

        public DocumentsController() : this(new TranslateableGenericRepository<Document, DocumentTranslation>())
        {

        }

        public DocumentsController(TranslateableGenericRepository<Document, DocumentTranslation> db)
        {
            this._db = db;
        }

        // GET: BackOffice/Documents
        public async Task<ActionResult> Index(int pageNumber = 1, int authorId = 0, int collectionId = 0)
        {
            var query = await _db.QueryAsync(d => (authorId == 0 || d.AuthorId == authorId) && 
                                             (collectionId == 0 || d.CollectionId == collectionId));

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

            Document document = await _db.GetByIdAsync(id);

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
            if (collectionId != null && _db.Set<Collection>().FirstOrDefault(c => c.Id == collectionId) != null)
            {
                doc.CatalogCode = CodeGenerator.SuggestDocumentCode(collectionId.Value);
                doc.CollectionId = collectionId.Value;
            }

            // Check for an author.
            if (authorId != null && _db.Set<Author>().FirstOrDefault(a => a.Id == authorId) != null)
            {
                doc.AuthorId = authorId.Value;
            }

            var model =  GenerateViewModel(doc);

            return View(model);
        }

        // POST: BackOffice/Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Document document)
        {
            if (ModelState.IsValid)
            {
                _db.Add(document);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(document));
        }

        // GET: BackOffice/Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = await _db.GetByIdAsync(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            document.Translations = document.Translations.ToList();

            return View(GenerateViewModel(document));
        }

        // POST: BackOffice/Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                foreach (var t in document.Translations)
                {
                    _db.UpdateTranslation(t);
                }

                _db.Update(document);

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(document));
        }

        // GET: BackOffice/Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await _db.GetByIdAsync(id);

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
            await _db.RemoveByIdAsync(id);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private DocumentEditViewModel GenerateViewModel(Document d)
        {
            var model = new DocumentEditViewModel();
            model.Document = d;

            model.AvailableAuthors = _db.Set<Author>().Select(a => new SelectListItem
                {
                    Text = a.LastName + ", " + a.FirstName,
                    Value = a.Id.ToString(),
                    Selected = d.AuthorId == a.Id
                })
                .ToList();

            model.AvailableAuthors.Insert(0, new SelectListItem
                {
                    Text = UiPrompts.ChooseOne,
                    Value = String.Empty,
                    Selected = true
                });

            model.AvailableCollections = _db.Set<Collection>()
                .ToList()
                .Select(c => new TranslatedViewModel<Collection, CollectionTranslation>(c))
                .Select(c => new SelectListItem
                {
                    Selected = c.Entity.Id == d.CollectionId,
                    Value = c.Entity.Id.ToString(),
                    Text = c.Entity.CatalogCode + " - " + "'" + c.Translation.Title + "'"
                })
                .ToList();

            model.AvailableCollections.Insert(0, new SelectListItem
            {
                Text = UiPrompts.ChooseOne,
                Value = String.Empty,
                Selected = true
            });

            return model;
        }

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doc = await _db.GetByIdAsync(id);

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
                _db.AddTranslation(translation);

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(new DocumentEditViewModel
                {
                    Document = translation.Document
                });
        }

        public async Task<ActionResult> SuggestCode(int? collectionId)
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
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}