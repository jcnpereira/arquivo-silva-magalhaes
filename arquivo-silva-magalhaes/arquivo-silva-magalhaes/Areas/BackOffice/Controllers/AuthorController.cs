using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Utilitites;
using ArquivoSilvaMagalhaes.Resources;
using System.Threading;
using System.Globalization;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class AuthorController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/Author/
        public async Task<ActionResult> Index()
        {
            return View(await db.AuthorSet.ToListAsync());
        }

        // GET: /BackOffice/Author/Details/5
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

        // GET: /BackOffice/Author/Create
        public ActionResult Create()
        {
            var textList = new List<AuthorTextEditModel>
            {
                new AuthorTextEditModel { LanguageCode = "pt", DisplayLanguageName = "Português" },
                new AuthorTextEditModel { LanguageCode = "en", DisplayLanguageName = "Inglês" }
            };

            // var model = new AuthorEditModel();
            var tupleModel = new Tuple<AuthorEditModel, List<AuthorTextEditModel>>(new AuthorEditModel(), textList);

            return View(tupleModel);
        }

        // TODO: DOESN'T WORK
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(Tuple<AuthorEditModel, List<AuthorTextEditModel>> tupleModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var author = new Author
        //        {
        //            FirstName = tupleModel.Item1.FirstName,
        //            LastName = tupleModel.Item1.LastName,
        //            BirthDate = tupleModel.Item1.BirthDate,
        //            DeathDate = DateTime.Now
        //        };

                
        //    }


        //    return View(tupleModel);
        //}

        // POST: /BackOffice/Author/Create
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
                    DeathDate = DateTime.Now
                };

                author.AuthorTexts = new HashSet<AuthorText>
                {
                    new AuthorText { Author = author, LanguageCode = "pt", Biography = model.BiographyPt, Nationality = model.NationalityPt, Curriculum = model.CurriculumPt },
                    
                };

                if (model.BiographyEn != null && model.NationalityEn != null && model.CurriculumEn != null)
                {
                    author.AuthorTexts.Add(
                        new AuthorText { Author = author, LanguageCode = "en", Biography = model.BiographyEn, Nationality = model.NationalityEn, Curriculum = model.CurriculumEn }
                    );
                }



                //author.AuthorTexts = model.AuthorTextEditModels
                //    .Select(textmodel => new AuthorText
                //    {
                //        Author = author,
                //        Biography = textmodel.Biography,
                //        Curriculum = textmodel.Curriculum,
                //        LanguageCode = textmodel.LanguageCode,
                //        Nationality = textmodel.Nationality
                //    }).ToList();

                db.AuthorSet.Add(author);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /BackOffice/Author/Edit/5
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

        // POST: /BackOffice/Author/Edit/5
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

        // GET: /BackOffice/Author/Delete/5
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

        // POST: /BackOffice/Author/Delete/5
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
