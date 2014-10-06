using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.ViewModels;
using ImageResizer;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class BannerPhotographController : SiteControllerBase
    {
        private ITranslateableRepository<Banner, BannerTranslation> db;

        public BannerPhotographController()
            : this(new TranslateableRepository<Banner, BannerTranslation>()) { }

        public BannerPhotographController(ITranslateableRepository<Banner, BannerTranslation> db)
        {
            this.db = db;
        }

        // GET: /BackOffice/BannerPhotograph/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            var model = await db.Entities
                .Include(b => b.Translations)
                .OrderBy(b => b.Id)
                .Select(b => new TranslatedViewModel<Banner, BannerTranslation>
                {
                    Entity = b
                })
                .ToPagedListAsync(pageNumber, 9);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var banner = await db.GetByIdAsync(id);

            if (banner == null)
            {
                return HttpNotFound();
            }

            return View(banner);
        }


        // GET: /BackOffice/BannerPhotograph/Create
        public ActionResult Create()
        {
            var banner = new Banner();

            banner.Translations.Add(new BannerTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            return View(new BannerPhotographEditViewModel(banner));
        }

        // POST: /BackOffice/BannerPhotograph/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BannerPhotographEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

                FileUploadHelper.SaveImage(
                    model.Image.InputStream,
                    1024,
                    500,
                    Path.Combine(Server.MapPath("~/Public/Banners"), newName),
                    FitMode.Crop);

                model.Banner.UriPath = newName;

                db.Add(model.Banner);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /BackOffice/BannerPhotograph/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var banner = await db.GetByIdAsync(id);

            if (banner == null)
            {
                return HttpNotFound();
            }

            return View(new BannerPhotographEditViewModel(banner));
        }

        // POST: /BackOffice/BannerPhotograph/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BannerPhotographEditViewModel model)
        {
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                db.Update(model.Banner);
                db.ExcludeFromUpdate(model.Banner, b => new { b.UriPath });

                if (model.Image != null)
                {
                    var fileName = db.GetValueFromDb(model.Banner, b => b.UriPath);

                    FileUploadHelper.SaveImage(
                        model.Image.InputStream,
                        1024,
                        500,
                        Path.Combine(Server.MapPath("~/Public/Banners"), fileName),
                        FitMode.Crop);
                }

                foreach (var item in model.Banner.Translations)
                {
                    db.UpdateTranslation(item);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /BackOffice/BannerPhotograph/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner bannerphotograph = await db.GetByIdAsync(id);
            if (bannerphotograph == null)
            {
                return HttpNotFound();
            }
            return View(bannerphotograph);
        }

        // POST: /BackOffice/BannerPhotograph/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.RemoveByIdAsync(id);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #region Translation Actions
        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var e = await db.GetByIdAsync(id);

            if (e == null)
            {
                return HttpNotFound();
            }

            if (e.Translations.Count == LanguageDefinitions.Languages.Count)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                e.Translations.Select(t => t.LanguageCode).ToArray());

            return View(new BannerTranslation
            {
                BannerPhotographId = e.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(BannerTranslation translation)
        {
            var author = await db.GetByIdAsync(translation.BannerPhotographId);

            if (ModelState.IsValid)
            {
                author.Translations.Add(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                author.Translations.Select(t => t.LanguageCode).ToArray());

            return View(translation);
        }

        public async Task<ActionResult> DeleteTranslation(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode) || languageCode == LanguageDefinitions.DefaultLanguage)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            return View(tr);
        }

        [HttpPost]
        [ActionName("DeleteTranslation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTranslationConfirmed(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            await db.RemoveTranslationByIdAsync(id, languageCode);

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        #endregion

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
