using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ImagesController : ArchiveControllerBase
    {
        private ITranslateableEntityRepository<Image, ImageTranslation> _db;

        public ImagesController() : this(new TranslateableGenericRepository<Image, ImageTranslation>())
        {

        }

        public ImagesController(ITranslateableEntityRepository<Image, ImageTranslation> db)
        {
            this._db = db;
        }

        // GET: BackOffice/Image
        public async Task<ActionResult> Index(int pageNumber = 1, int documentId = 0)
        {
            var query = await _db.QueryAsync(i => documentId == 0 || i.DocumentId == documentId);

            return View(query
                .Select(i => new TranslatedViewModel<Image, ImageTranslation>(i))
                .ToPagedList(pageNumber, 10));
        }

        public async Task<ActionResult> Details(int id)
        {
            var image = await _db.GetByIdAsync(id);

            if (image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            image.Translations = image.Translations.ToList();

            return View(image);
        }

        public ActionResult Create(int? documentId)
        {
            var image = new Image();

            image.Translations.Add(new ImageTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            

            return View(GenerateViewModel(image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    var path = Server.MapPath("~/Public/Images/");
                    FileUploadHelper.GenerateVersions(model.ImageUpload.InputStream, path + fileName);

                    model.Image.ImageUrl = fileName;
                }

                model.Image.Keywords = 
                    _db.Set<Keyword>()
                       .Where(kw => model.KeywordIds.Contains(kw.Id))
                       .ToList();

                _db.Add(model.Image);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = await _db.GetByIdAsync(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            return View(GenerateViewModel(image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ImageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Update(model.Image);

                // "Force-load" the image and the keywords.
                await _db.ForceLoadAsync(model.Image, i => i.Keywords);

                model.Image.Keywords = _db.Set<Keyword>()
                                          .Where(k => model.KeywordIds.Contains(k.Id)).ToList();

                foreach (var t in model.Image.Translations)
                {
                    _db.UpdateTranslation(t);
                }

                if (model.ImageUpload != null)
                {
                    var fileName =
                        _db.GetValueFromDb(model.Image, i => i.ImageUrl) ??
                        Guid.NewGuid().ToString() + "_" + Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);

                    var path = Server.MapPath("~/Public/Images/");
                    Directory.CreateDirectory(path);

                    FileUploadHelper.GenerateVersions(model.ImageUpload.InputStream, path + fileName);

                    model.Image.ImageUrl = fileName;
                }
                else
                {
                    _db.ExcludeFromUpdate(model.Image, i => new { i.ImageUrl });
                }

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(model.Image));
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = await _db.GetByIdAsync(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            image.Translations = image.Translations.ToList();

            return View(image);
        }

        // POST: /BackOffice/Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _db.RemoveByIdAsync(id);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SuggestCode(int? documentId)
        {
            if (documentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var d = _db.Set<Document>().FirstOrDefault(doc => doc.Id == documentId.Value);

            if (d == null)
            {
                return HttpNotFound();
            }

            return Json(CodeGenerator.SuggestImageCode(d.Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Populates the ViewBag with select list items for the
        /// drop-down lists required for this entity.
        /// </summary>
        /// <param name="keywordIds"></param>
        /// <param name="documentId"></param>
        private ImageEditViewModel GenerateViewModel(Image image)
        {
            var model = new ImageEditViewModel();
            model.Image = image;

            model.AvailableDocuments = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = UiPrompts.ChooseOne,
                    Value = String.Empty,
                    Selected = true
                }
            };

            model.AvailableDocuments.AddRange(
                _db.Set<Document>()
                .OrderBy(d => d.Id)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.CatalogCode + " - " + d.Title,
                    Selected = d.Id == image.DocumentId
                }));

            var keywordIds = image.Keywords.Select(k => k.Id).ToList();

            model.AvailableKeywords = new List<SelectListItem>();

            model.AvailableKeywords
                .AddRange(_db.Set<Keyword>()
                .OrderBy(k => k.Id)
                .ToList()
                .Select(k => new TranslatedViewModel<Keyword, KeywordTranslation>(k))
                .Select(k => new SelectListItem
                {
                    Value = k.Entity.Id.ToString(),
                    Text = k.Translation.Value,
                    Selected = keywordIds.Contains(k.Entity.Id)
                }));

            model.AvailableClassifications = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "",
                    Text = UiPrompts.ChooseOne,
                    Selected = true
                }
            };

            model.AvailableClassifications
                .AddRange(_db.Set<Classification>()
                .OrderBy(k => k.Id)
                .ToList()
                .Select(k => new TranslatedViewModel<Classification, ClassificationTranslation>(k))
                .Select(k => new SelectListItem
                {
                    Value = k.Entity.Id.ToString(),
                    Text = k.Translation.Value,
                    Selected = keywordIds.Contains(k.Entity.Id)
                }));

            return model;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}