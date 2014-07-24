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
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Utilitites;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class DocumentsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Documents
        public async Task<ActionResult> Index()
        {
            return View(await db.Documents.ToListAsync());
        }

        // GET: BackOffice/Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = await db.Documents.FindAsync(id);

            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: BackOffice/Documents/Create
        public async Task<ActionResult> Create(int? CollectionId, int? AuthorId)
        {
            var doc = new Document();

            doc.Translations.Add(new DocumentTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            // Check for an author.
            if (AuthorId != null && db.Authors.Find(AuthorId) != null)
            {
                doc.AuthorId = AuthorId.Value;
            }

            // Check for a collection.
            if (CollectionId != null && db.Collections.Find(CollectionId) != null)
            {
                doc.CollectionId = CollectionId.Value;
            }

            var model = GenerateViewModel(doc);

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

                db.Documents.Add(document);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: BackOffice/Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(GenerateViewModel(document));
        }

        // POST: BackOffice/Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ResponsibleName,DocumentDate,CatalogationDate,Notes,CatalogCode")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: BackOffice/Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
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
            Document document = await db.Documents.FindAsync(id);
            db.Documents.Remove(document);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private DocumentEditViewModel GenerateViewModel(Document d)
        {
            var model = new DocumentEditViewModel();
            model.Document = d;

            model.AvailableAuthors = db.Authors.Select(a => new SelectListItem
                {
                    Text = a.LastName + ", " + a.FirstName,
                    Value = a.Id.ToString(),
                    Selected = d.AuthorId == a.Id
                })
                .ToList();

            var query = from c in db.Collections
                        join ct in db.CollectionTranslations on c.Id equals ct.CollectionId
                        where ct.LanguageCode == LanguageDefinitions.DefaultLanguage
                        select new SelectListItem
                        {
                            Selected = d.CollectionId == c.Id,
                            Value = c.Id.ToString(),
                            Text = c.CatalogCode + " - " + ct.Title
                        };

            model.AvailableCollections = query.ToList();

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
