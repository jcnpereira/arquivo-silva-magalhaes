using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class PortalController : SiteControllerBase
    {
        private ITranslateableRepository<Archive, ArchiveTranslation> db;

        public PortalController()
            : this(new TranslateableGenericRepository<Archive, ArchiveTranslation>())
        {

        }

        public PortalController(ITranslateableRepository<Archive, ArchiveTranslation> db)
        {
            this.db = db;
        }


        // GET: /Portal/
        public ActionResult Index()
        {
            return View(db.Set<Archive>().FirstOrDefault());
        }

        // GET: /Portal/Edit/5
        //public async Task<ActionResult> Edit()
        public ActionResult Edit()
        {
            return View(db.Set<Archive>().FirstOrDefault());
        }

        // POST: /Portal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Archive model)
        {
            if (ModelState.IsValid)
            {
                db.Update(model);

                foreach (var t in model.Translations)
                {
                    db.UpdateTranslation(t);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //public async Task<ActionResult> AddTranslation()
        public ActionResult AddTranslation()
        {
            var archive = db.Set<Archive>().FirstOrDefault();

            var t = new ArchiveTranslation
            {
                ArchiveId = archive.Id
            };

            ViewBag.Languages =
                LanguageDefinitions.GenerateAvailableLanguageDDL(archive.Translations.Select(tr => tr.LanguageCode));

            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(ArchiveTranslation t)
        {
            if (ModelState.IsValid)
            {
                db.AddTranslation(t);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(t);
        }

        // GET: /Portal/Delete/5
        public async Task<ActionResult> DeleteTranslation(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var t = await db.GetTranslationAsync(1, languageCode);

            if (t == null)
            {
                return HttpNotFound();
            }

            return View(t);
        }

        // POST: /Portal/Delete/5
        [HttpPost, ActionName("DeleteTranslation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTranslationConfirmed(string languageCode)
        {
            var archive = db.Set<Archive>().FirstOrDefault();

            await db.RemoveTranslationByIdAsync(archive.Id, languageCode);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
