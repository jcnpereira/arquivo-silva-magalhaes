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
    public class AuthorController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Author
        public async Task<ActionResult> Index()
        {
            return View(await db.AuthorSet.ToListAsync());
        }

        // GET: BackOffice/Author/Details/5
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

            var model = new AuthorViewModel(author);

            return View(model);
        }

        // GET: BackOffice/Author/Create
        public ActionResult Create()
        {
            return View(new AuthorEditModel());
        }

        // POST: BackOffice/Author/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AuthorEditModel model)
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

                var texts = new HashSet<AuthorText>
                {
                    new AuthorText
                    {
                        Author = author,
                        LanguageCode = "pt",
                        Nationality = model.NationalityPt,
                        Curriculum = model.CurriculumPt,
                        Biography = model.BiographyPt
                    },
                    
                };

                if (model.NationalityEn != null && model.CurriculumEn != null && model.BiographyEn != null)
                {
                    // Only add the english content if any exists.
                    if (model.NationalityEn.Trim().Length != 0 &&
                        model.CurriculumEn.Trim().Length != 0 &&
                        model.CurriculumEn.Trim().Length != 0)
                    {
                        texts.Add(
                            new AuthorText
                            {
                                Author = author,
                                LanguageCode = "en",
                                Nationality = model.NationalityEn,
                                Curriculum = model.CurriculumEn,
                                Biography = model.BiographyEn
                            }
                        );
                    }
                }

                db.AuthorSet.Add(author);

                await db.SaveChangesAsync();

                db.AuthorTextSet.AddRange(texts);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BackOffice/Author/Edit/5
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
            return View(new AuthorEditModel(author));
        }

        // POST: BackOffice/Author/Edit/5
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

        // GET: BackOffice/Author/Delete/5
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

        // POST: BackOffice/Author/Delete/5
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
