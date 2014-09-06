﻿using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.ViewModels;
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

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class CollectionsController : ArchiveControllerBase
    {
        private ITranslateableRepository<Collection, CollectionTranslation> db;

        public CollectionsController()
            : this(new TranslateableGenericRepository<Collection, CollectionTranslation>()) { }

        public CollectionsController(TranslateableGenericRepository<Collection, CollectionTranslation> db)
        {
            this.db = db;
        }

        // GET: /BackOffice/Collection/
        public async Task<ActionResult> Index(int authorId = 0, int pageNumber = 1)
        {
            var query = db.Entities
                          .Where(c => authorId == 0 || c.Authors.Any(a => a.Id == authorId));

            return View((await query.ToListAsync())
                .Select(c => new TranslatedViewModel<Collection, CollectionTranslation>(c))
                .ToPagedList(pageNumber, 10));
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
        public ActionResult Create()
        {
            var model = new CollectionEditViewModel();

            var c = new Collection();
            c.Translations.Add(new CollectionTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            return View(GenerateViewModel(c));
        }

        // POST: /BackOffice/Collection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CollectionEditViewModel model)
        {
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
                            Guid.NewGuid().ToString() + "_" + model.Logo.FileName;

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
            await db.RemoveByIdAsync(id);
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