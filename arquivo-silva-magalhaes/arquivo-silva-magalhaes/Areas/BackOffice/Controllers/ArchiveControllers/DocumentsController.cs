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
    public class DocumentsController : Controller
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
            var model = new DocumentEditViewModel
            {
                //AvailableKeywords = await db.KeywordTranslations
                //    .Where(kt => kt.LanguageCode == LanguageDefinitions.DefaultLanguage)
                //    .Select(kt => new SelectListItem
                //    {
                //        Value = kt.KeywordId.ToString(),
                //        Text = kt.Value
                //    }).ToListAsync(),

                Translations = new List<DocumentTranslationEditViewModel>
                {
                    new DocumentTranslationEditViewModel { LanguageCode = LanguageDefinitions.DefaultLanguage }
                }
            };

            // Check for an author.
            if (AuthorId != null && db.Authors.Find(AuthorId) != null)
            {
                model.AuthorId = AuthorId.Value;
            }
            else
            {
                model.AvailableAuthors = await db.Authors
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.FirstName + " " + a.LastName
                    }).ToListAsync();
            }
            // Check for a collection.
            if (CollectionId != null && db.Collections.Find(CollectionId) != null)
            {
                model.CollectionId = CollectionId.Value;
            }
            else
            {
                model.AvailableCollections = await db.Collections
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.CatalogCode
                    }).ToListAsync();
            }

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
                document.Keywords = document.KeywordIds.Select(kid => db.Keywords.Find(kid)).ToList();

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
            return View(document);
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
