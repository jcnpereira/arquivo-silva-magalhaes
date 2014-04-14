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
            var model = new AuthorEditViewModel();

            model.I18nModel = new AuthorI18nViewModel
            {
                AvailableLanguages = new List<SelectListItem>
                {
                    new SelectListItem { Value = "pt", Selected = true, Text = "Português" }
                }
            };

            return View(model);
        }

        // POST: BackOffice/Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                       LanguageCode = model.LanguageCode,
                       Nationality = model.Nationality,
                       Curriculum = model.Curriculum,
                       Biography = model.Biography
                   }
                );

                db.AuthorSet.Add(author);
                await db.SaveChangesAsync();
                // return RedirectToAction("Index");
                ViewBag.Id = author.Id;
                return View("AddMoreLanguagesPrompt");
            }

            return View(model);
        }

        public async Task<ActionResult> AddLanguage(int? Id)
        {
            if (Id != null)
            {
                var author = await db.AuthorSet.FindAsync(Id);

                if (author == null)
                {
                    return HttpNotFound();
                }

                return View(new AuthorI18nViewModel
                {
                    AuthorId = author.Id,
                    AvailableLanguages = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "en", Text = "Inglês", Selected = true }
                    }
                });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddLanguage(AuthorI18nViewModel model)
        {
            if (ModelState.IsValid)
            {
                var text = new AuthorText
                {
                    Author = db.AuthorSet.Find(model.AuthorId),
                    LanguageCode = model.LanguageCode,
                    Nationality = model.Nationality,
                    Biography = model.Biography,
                    Curriculum = model.Curriculum
                };

                db.AuthorTextSet.Add(text);
                await db.SaveChangesAsync();

                ViewBag.AuthorId = model.AuthorId;

                return View("AddMoreLanguagesPrompt");
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
