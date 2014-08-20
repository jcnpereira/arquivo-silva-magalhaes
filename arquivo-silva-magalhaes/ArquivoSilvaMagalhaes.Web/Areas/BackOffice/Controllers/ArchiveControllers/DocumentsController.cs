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

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class DocumentsController : ArchiveController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/Documents
        public async Task<ActionResult> Index(int pageNumber = 1, int authorId = 0, int collectionId = 0)
        {
            var query = _db.Documents
                           .Include(dt => dt.Author)
                           .Include(dt => dt.Collection);

            if (authorId > 0)
            {
                query = query.Where(d => d.AuthorId == authorId);
            }

            return View(await Task.Run(() =>
                query
                   .OrderBy(d => d.Id)
                   .ToPagedList(pageNumber, 10)));
        }

        // GET: BackOffice/Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = await _db.Documents.FindAsync(id);
            
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
            if (collectionId != null && _db.Collections.Find(collectionId) != null)
            {
                doc.CatalogCode = CodeGenerator.SuggestDocumentCode(collectionId.Value);
                doc.CollectionId = collectionId.Value;
            }

            // Check for an author.
            if (authorId != null && _db.Authors.Find(authorId) != null)
            {
                doc.AuthorId = authorId.Value;
            }

            var model = await GenerateViewModel(doc);

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
                _db.Documents.Add(document);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(await GenerateViewModel(document));
        }

        // GET: BackOffice/Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await _db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(await GenerateViewModel(document));
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
                _db.Entry(document).State = EntityState.Modified;

                foreach (var t in document.Translations)
                {
                    _db.Entry(t).State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(await GenerateViewModel(document));
        }

        // GET: BackOffice/Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await _db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: BackOffice/Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Document document = await _db.Documents.FindAsync(id);
            _db.Documents.Remove(document);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<DocumentEditViewModel> GenerateViewModel(Document d)
        {
            var model = new DocumentEditViewModel();
            model.Document = d;

            model.AvailableAuthors = await _db.Authors.Select(a => new SelectListItem
                {
                    Text = a.LastName + ", " + a.FirstName,
                    Value = a.Id.ToString(),
                    Selected = d.AuthorId == a.Id
                })
                .ToListAsync();

            model.AvailableAuthors.Insert(0, new SelectListItem
                {
                    Text = UiPrompts.ChooseOne,
                    Value = String.Empty,
                    Selected = true
                });

            var query = _db.Collections
                .Join(_db.CollectionTranslations, (c) => c.Id, (ct) => ct.CollectionId, (c, ct) => new SelectListItem
                {
                    Selected = d.CollectionId == c.Id,
                    Value = c.Id.ToString(),
                    Text = c.CatalogCode + " - " + ct.Title
                });

            model.AvailableCollections = await query.ToListAsync();

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
            var doc = await _db.Documents.FindAsync(id);

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

            ViewBag.AvailableLanguages =
                LanguageDefinitions.Languages
                                   .Where(t => !translations.Contains(t))
                                   .Select(t => new SelectListItem
                                   {
                                       Value = t,
                                       Text = LanguageDefinitions.GetLanguage(t)
                                   })
                                   .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(DocumentTranslation translation)
        {
            if (ModelState.IsValid)
            {
                _db.DocumentTranslations.Add(translation);

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
            var c = await _db.Collections.FindAsync(collectionId);

            if (c == null)
            {
                return HttpNotFound();
            }

            var collectionCode = c.CatalogCode;

            var docCodes = c.Documents.Select(d => d.CatalogCode).ToArray();

            Array.Sort(docCodes, new AlphaNumericComparator());

            var lastCode = docCodes.LastOrDefault();
            var lastCodeNumeric = 1;
            var suggestedCode = collectionCode + "-";

            if (lastCode == null)
            {
                suggestedCode = suggestedCode + "1";
            }
            else if (int.TryParse(lastCode, out lastCodeNumeric)) // number.
            {
                suggestedCode = suggestedCode + (lastCodeNumeric + 1).ToString();
            }
            else
            {
                suggestedCode = suggestedCode + (docCodes.Count() + 1).ToString();
            }

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