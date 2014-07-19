using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class AuthorsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Authors
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(db.Authors
                .ToList()
                .Select(a => new AuthorViewModel(a))
                .ToPagedList(pageNumber, 10)); // TODO Allow configs for page size.
        }

        // GET: BackOffice/Authors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var author = await db.Authors.FindAsync(id);

            if (author == null)
            {
                return HttpNotFound();
            }

            ViewBag.AreLanguagesMissing = author.Translations.Count < LanguageDefinitions.Languages.Count;

            return View(new AuthorViewModel(author));
        }

        // GET: BackOffice/Authors/Create
        public ActionResult Create()
        {
            var model = new AuthorEditViewModel
            {
                Translations = new List<AuthorTranslationEditViewModel> 
                { 
                    new AuthorTranslationEditViewModel { LanguageCode = LanguageDefinitions.DefaultLanguage } 
                }
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
                db.Authors.Add(author);

                await db.SaveChangesAsync();

                // We don't need more languages. Redirect to the list.
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: BackOffice/Authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Author author = await db.Authors.FindAsync(id);

            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var t = author.Translations
                .First(text => text.LanguageCode == LanguageDefinitions.DefaultLanguage);

            return View(new AuthorEditViewModel(author));
        }

        // POST: BackOffice/Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;

                foreach (var t in author.Translations)
                {
                    db.Entry(t).State = EntityState.Modified;
                }

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
            Author author = await db.Authors.FindAsync(id);
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
            Author author = await db.Authors.FindAsync(id);
            db.Authors.Remove(author);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> EditText(int? AuthorId, string LanguageCode)
        {
            if (AuthorId == null || LanguageCode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.MustSpecifyContent);
            }

            AuthorTranslation text = await db.AuthorTranslations.FindAsync(AuthorId, LanguageCode);

            if (text == null)
            {
                return HttpNotFound();
            }

            return View(text);
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
