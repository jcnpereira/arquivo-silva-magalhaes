using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ShowcasePhotoesController : ArchiveControllerBase
    {
        private ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db;

        public ShowcasePhotoesController()
            : this(new TranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation>()) { }

        public ShowcasePhotoesController(ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/ShowcasePhotoes
        public async Task<ActionResult> Index(int pageNumber = 1, string query = "", int imageId = 0)
        {
            var model = await db.Entities
                .Include(sp => sp.Translations)
                .Where(sp => query == "" || sp.Translations.Any(t => t.Title.Contains(query)))
                .Where(sp => imageId == 0 || sp.ImageId == imageId)
                .OrderByDescending(b => b.VisibleSince)
                .Select(p => new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>
                {
                    Entity = p
                })
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.ImageId = imageId;
            ViewBag.Query = query;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        // GET: BackOffice/ShowcasePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var showcasePhoto = await db.GetByIdAsync(id);

            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }

            showcasePhoto.Translations = showcasePhoto.Translations.ToList();

            return View(showcasePhoto);
        }

        // GET: BackOffice/ShowcasePhotoes/Create
        public async Task<ActionResult> Create(int? imageId)
        {
            var photo = new ShowcasePhoto();

            if (imageId != null && await db.Set<Image>()
                .AnyAsync(i =>
                    i.Id == imageId &&
                    i.ImageUrl != null && i.IsVisible && i.Document.Collection.IsVisible))
            {
                photo.ImageId = imageId.Value;
            }

            photo.Translations.Add(new ShowcasePhotoTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            return View(GenerateViewModel(photo));
        }

        // POST: BackOffice/ShowcasePhotoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShowcasePhoto showcasePhoto)
        {
            var image = db.Set<Image>().FirstOrDefault(i => i.Id == showcasePhoto.ImageId);

            if (!image.IsVisible)
            {
                ModelState.AddModelError("ImageId", ShowcasePhotoStrings.ValidationError_ImageHidden);
            }

            if (ModelState.IsValid)
            {
                db.Add(showcasePhoto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(showcasePhoto));
        }

        // GET: BackOffice/ShowcasePhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await db.GetByIdAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            return View(GenerateViewModel(showcasePhoto));
        }

        // POST: BackOffice/ShowcasePhotoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShowcasePhoto showcasePhoto)
        {
            if (ModelState.IsValid)
            {
                db.Update(showcasePhoto);

                foreach (var item in showcasePhoto.Translations)
                {
                    db.UpdateTranslation(item);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(GenerateViewModel(showcasePhoto));
        }

        // GET: BackOffice/ShowcasePhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await db.GetByIdAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }

            showcasePhoto.Translations = showcasePhoto.Translations.ToList();

            return View(showcasePhoto);
        }

        // POST: BackOffice/ShowcasePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.RemoveByIdAsync(id);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private ShowcasePhotoEditViewModel GenerateViewModel(ShowcasePhoto photo)
        {
            var model = new ShowcasePhotoEditViewModel
            {
                ShowcasePhoto = photo
            };

            model.AvailableImages = db.Set<Image>()
                .Where(i => 
                    i.IsVisible &&
                    i.Document.Collection.IsVisible &&
                    i.ImageUrl != null &&
                    i.ImageUrl != "")
                .ToList()
                .Select(i => new TranslatedViewModel<Image, ImageTranslation>(i))
                .Select(i => new SelectListItem
                {
                    Value = i.Entity.Id.ToString(),
                    Text = i.Translation.Title
                });

            return model;
        }

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var showcasePhoto = await db.GetByIdAsync(id);

            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }

            if (showcasePhoto.Translations.Count == LanguageDefinitions.Languages.Count)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                showcasePhoto.Translations.Select(t => t.LanguageCode).ToArray());

            return View(new ShowcasePhotoTranslation
            {
                ShowcasePhotoId = showcasePhoto.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(ShowcasePhotoTranslation translation)
        {
            var showcasePhoto = await db.GetByIdAsync(translation.ShowcasePhotoId);

            if (ModelState.IsValid)
            {
                showcasePhoto.Translations.Add(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                showcasePhoto.Translations.Select(t => t.LanguageCode).ToArray());

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