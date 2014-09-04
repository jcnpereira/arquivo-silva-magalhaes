using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ShowcasePhotoesController : ArchiveControllerBase
    {
        private ITranslateableEntityRepository<ShowcasePhoto, ShowcasePhotoTranslation> _db;

        public ShowcasePhotoesController()
            : this(new TranslateableGenericRepository<ShowcasePhoto, ShowcasePhotoTranslation>()) { }

        public ShowcasePhotoesController(ITranslateableEntityRepository<ShowcasePhoto, ShowcasePhotoTranslation> db)
        {
            this._db = db;
        }

        // GET: BackOffice/ShowcasePhotoes
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await _db.GetAllAsync())
                .OrderByDescending(p => p.VisibleSince)
                .Select(p => new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>(p))
                .ToPagedList(pageNumber, 10));
        }

        // GET: BackOffice/ShowcasePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var showcasePhoto = await _db.GetByIdAsync(id);

            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }

            showcasePhoto.Translations = showcasePhoto.Translations.ToList();

            return View(showcasePhoto);
        }

        // GET: BackOffice/ShowcasePhotoes/Create
        public ActionResult Create(int? imageId)
        {
            var photo = new ShowcasePhoto();
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
            var image = _db.Set<Image>().FirstOrDefault(i => i.Id == showcasePhoto.ImageId);

            if (!image.IsVisible)
            {
                ModelState.AddModelError("ImageId", ShowcasePhotoStrings.ValidationError_ImageHidden);
            }

            if (ModelState.IsValid)
            {
                _db.Add(showcasePhoto);
                await _db.SaveChangesAsync();
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
            ShowcasePhoto showcasePhoto = await _db.GetByIdAsync(id);
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
                _db.Update(showcasePhoto);

                foreach (var item in showcasePhoto.Translations)
                {
                    _db.Update(showcasePhoto);
                }

                await _db.SaveChangesAsync();
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
            ShowcasePhoto showcasePhoto = await _db.GetByIdAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            return View(showcasePhoto);
        }

        // POST: BackOffice/ShowcasePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _db.RemoveByIdAsync(id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private ShowcasePhotoEditViewModel GenerateViewModel(ShowcasePhoto photo)
        {
            var model = new ShowcasePhotoEditViewModel
            {
                ShowcasePhoto = photo
            };

            model.AvailableImages = _db.Set<Image>()
                .Where(i => i.IsVisible && i.ImageUrl != null && i.ImageUrl != "")
                .Select(i => new TranslatedViewModel<Image, ImageTranslation>(i))
                .Select(i => new SelectListItem
                {
                    Value = i.Entity.ToString(),
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

            var showcasePhoto = await _db.GetByIdAsync(id);

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
            var showcasePhoto = await _db.GetByIdAsync(translation.ShowcasePhotoId);

            if (ModelState.IsValid)
            {
                showcasePhoto.Translations.Add(translation);
                await _db.SaveChangesAsync();

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

            var tr = await _db.GetTranslationAsync(id.Value, languageCode);

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

            var tr = await _db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            await _db.RemoveTranslationByIdAsync(id, languageCode);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}