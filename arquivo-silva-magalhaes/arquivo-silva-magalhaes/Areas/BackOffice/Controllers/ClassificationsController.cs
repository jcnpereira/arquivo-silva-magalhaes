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
using ArquivoSilvaMagalhaes.Utilitites;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ClassificationsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Classifications
        public async Task<ActionResult> Index()
        {
            return View(await db.ClassificationSet.ToListAsync());
        }

        // GET: BackOffice/Classifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.ClassificationSet.FindAsync(id);
            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(classification);
        }

        // GET: BackOffice/Classifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Classifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassificationEditModel model)
        {
            if (ModelState.IsValid)
            {
                var classification = new Classification();

                classification.ClassificationTexts.Add(new ClassificationText
                    {
                        LanguageCode = LanguageDefinitions.DefaultLanguage,
                        Value = model.Classfication
                    });

                db.ClassificationSet.Add(classification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BackOffice/Classifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.ClassificationSet.FindAsync(id);
            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(new ClassificationEditModel
                {
                    Id = classification.Id,
                    Classfication = classification.ClassificationTexts.First(ct => ct.LanguageCode == LanguageDefinitions.DefaultLanguage).Value
                });
        }

        // POST: BackOffice/Classifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClassificationEditModel model)
        {
            if (ModelState.IsValid)
            {
                var classification = db.ClassificationSet.Find(model.Id);

                var t = classification.ClassificationTexts.First(ct => ct.LanguageCode == LanguageDefinitions.DefaultLanguage);

                t.Value = model.Classfication;

                db.Entry(t).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: BackOffice/Classifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classification classification = await db.ClassificationSet.FindAsync(id);
            if (classification == null)
            {
                return HttpNotFound();
            }
            return View(classification);
        }

        // POST: BackOffice/Classifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Classification classification = await db.ClassificationSet.FindAsync(id);
            db.ClassificationSet.Remove(classification);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult AddLanguage(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLanguage(ClassificationEditModel model)
        {
            return View();
        }

        public ActionResult EditLanguage(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLanguage(ClassificationEditModel model)
        {
            return View();
        }

        public ActionResult DeleteLanguage(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteLanguage")]
        public ActionResult DeleteLanguageConfirmed(int id)
        {
            return View();
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
