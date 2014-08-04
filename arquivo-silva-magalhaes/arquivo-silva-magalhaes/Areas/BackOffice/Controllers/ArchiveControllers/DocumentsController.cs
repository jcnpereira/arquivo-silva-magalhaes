using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Utilitites;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class DocumentsController : BackOfficeController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/Documents
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => _db.Documents.OrderBy(d => d.Id).ToPagedList(pageNumber, 10)));
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

            var query = _db.Collections
                .Join(_db.CollectionTranslations, (c) => c.Id, (ct) => ct.CollectionId, (c, ct) => new SelectListItem 
                {
                    Selected = d.CollectionId == c.Id,
                    Value = c.Id.ToString(),
                    Text = c.CatalogCode + " - " + ct.Title
                });

            model.AvailableCollections = await query.ToListAsync();

            

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

            var model = new DocumentTranslation
            {
                DocumentId = doc.Id
            };

            ViewBag.AvailableLanguages = new List<SelectListItem>
            {
                new SelectListItem { Value = "pt", Text = "português"},
                new SelectListItem { Value = "en", Text = "inglês"},
            };

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