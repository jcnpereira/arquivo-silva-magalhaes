using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ImageResizer;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.ViewModels;
using System.Diagnostics;

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
            return View((await db.Entities
                .OrderBy(b => b.Id)
                .ToListAsync())
                .Select(b => new TranslatedViewModel<Banner, BannerTranslation>(b))
                .ToPagedList(pageNumber, 10));
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
                var newName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Image.FileName);

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
