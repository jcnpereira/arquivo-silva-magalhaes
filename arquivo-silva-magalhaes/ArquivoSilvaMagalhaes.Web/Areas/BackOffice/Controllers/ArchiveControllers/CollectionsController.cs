using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.ViewModels;
using ImageResizer;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class CollectionsController : ArchiveControllerBase
    {
        private ITranslateableRepository<Collection, CollectionTranslation> db;

        public CollectionsController()
            : this(new TranslateableRepository<Collection, CollectionTranslation>()) { }

        public CollectionsController(TranslateableRepository<Collection, CollectionTranslation> db)
        {
            this.db = db;
        }

        // GET: /BackOffice/Collection/
        public async Task<ActionResult> Index(int pageNumber = 1, int authorId = 0, string query = "")
        {
            var model = await db.Entities
                .Include(c => c.Translations)
                .Where(c => authorId == 0 || c.Authors.Any(a => a.Id == authorId))
                .Where(c => query == "" || c.CatalogCode.Contains(query) || c.Translations.Any(t => t.Title.Contains(query)))
                .OrderBy(c => c.Id)
                .Select(col => new TranslatedViewModel<Collection, CollectionTranslation>
                {
                    Entity = col
                })
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.Query = query;
            ViewBag.AuthorId = authorId;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        // GET: /BackOffice/Collection/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Collection collection = await db.GetByIdAsync(id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            collection.Translations = collection.Translations.ToList();

            return View(collection);
        }

        // GET: /BackOffice/Collection/Create
        public async Task<ActionResult> Create(int authorId = 0)
        {
            var model = new CollectionEditViewModel();

            var c = new Collection();
            c.Translations.Add(new CollectionTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            if (authorId != 0)
            {
                c.Authors = await db.Set<Author>().Where(a => a.Id == authorId).ToListAsync();
            }

            return View(GenerateViewModel(c));
        }

        // POST: /BackOffice/Collection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CollectionEditViewModel model)
        {
            if (DoesCodeAlreadyExist(model.Collection))
            {
                ModelState.AddModelError("Collection.CatalogCode", CollectionStrings.Validation_CodeAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                if (model.Logo != null)
                {
                    var newName = Guid.NewGuid().ToString() + ".jpg";

                    FileUploadHelper.SaveImage(model.Logo.InputStream,
                        400, 400,
                        Server.MapPath("~/Public/Collections/") + newName,
                        FitMode.Crop);

                    model.Collection.LogoLocation = newName;
                }

                var authors = db.Set<Author>()
                                .Where(a => model.AuthorIds.Contains(a.Id));

                model.Collection.Authors = authors.ToList();

                db.Add(model.Collection);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            model.AvailableAuthors = db.Set<Author>()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.LastName + ", " + a.FirstName,
                    Selected = model.AuthorIds.Contains(a.Id)
                })
                .ToList();

            return View(model);
        }

        // GET: /BackOffice/Collection/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.GetByIdAsync(id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            collection.Translations = collection.Translations.ToList();

            var model = GenerateViewModel(collection);

            return View(model);
        }

        // POST: /BackOffice/Collection/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CollectionEditViewModel model)
        {
            if (DoesCodeAlreadyExist(model.Collection))
            {
                ModelState.AddModelError("Collection.CatalogCode", CollectionStrings.Validation_CodeAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                // Force-update the collection's author list.
                await db.ForceLoadAsync(model.Collection, c => c.Authors);

                model.Collection.Authors = db.Set<Author>()
                     .Where(a => model.AuthorIds.Contains(a.Id)).ToList();

                foreach (var t in model.Collection.Translations)
                {
                    db.UpdateTranslation(t);
                }

                // Update the logo if a new one is supplied. Don't allow property value changes if
                // the logo doesn't exist.
                if (model.Logo != null)
                {
                    var logo = db.GetValueFromDb(model.Collection, c => c.LogoLocation);

                    if (logo == null)
                    {
                        model.Collection.LogoLocation =
                            Guid.NewGuid().ToString() + ".jpg";

                        logo = model.Collection.LogoLocation;
                    }

                    FileUploadHelper.SaveImage(model.Logo.InputStream,
                        400, 400,
                        Server.MapPath("~/Public/Collections/") + logo,
                        FitMode.Crop);
                }
                else
                {
                    db.ExcludeFromUpdate(model.Collection, c => new { c.LogoLocation });
                }

                db.Update(model.Collection);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            model.AvailableAuthors = db.Set<Author>()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.LastName + ", " + a.FirstName,
                    Selected = model.AuthorIds.Contains(a.Id)
                })
                .ToList();

            return View(model);
        }

        // GET: /BackOffice/Collection/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.GetByIdAsync(id);

            if (collection == null)
            {
                return HttpNotFound();
            }

            collection.Translations = collection.Translations.ToList();

            return View(collection);
        }

        // POST: /BackOffice/Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var collection = await db.GetByIdAsync(id);

            db.Remove(collection);

            if (collection.LogoLocation != null)
            {
                var fullPath = Server.MapPath("~/Public/Collections/" + collection.LogoLocation);

                // Remove file from the disk.
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private CollectionEditViewModel GenerateViewModel(Collection c)
        {
            var vm = new CollectionEditViewModel();

            vm.Collection = c;

            var authorIds = c.Authors.Select(a => a.Id).ToList();

            vm.AvailableAuthors = db.Set<Author>()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.LastName + ", " + a.FirstName,
                    Selected = authorIds.Contains(a.Id)
                })
                .ToList();

            return vm;
        }

        #region Translation Actions
        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var c = await db.GetByIdAsync(id);

            if (c == null)
            {
                return HttpNotFound();
            }

            ViewBag.Languages =
                LanguageDefinitions.GenerateAvailableLanguageDDL(c.Translations.Select(t => t.LanguageCode).ToList());

            return View(new CollectionTranslation
                {
                    CollectionId = c.Id
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(CollectionTranslation translation)
        {
            if (ModelState.IsValid)
            {
                db.AddTranslation(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

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

        private bool DoesCodeAlreadyExist(Collection col)
        {
            return db.Entities
                .Any(d => d.CatalogCode == col.CatalogCode && d.Id != col.Id);
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