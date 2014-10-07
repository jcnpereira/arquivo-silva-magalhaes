using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ImagesController : ArchiveControllerBase
    {
        private ITranslateableRepository<Image, ImageTranslation> db;

        public ImagesController()
            : this(new TranslateableRepository<Image, ImageTranslation>()) { }

        public ImagesController(ITranslateableRepository<Image, ImageTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Image
        public async Task<ActionResult> Index(
            int collectionId = 0,
            int documentId = 0,
            int keywordId = 0,
            int classificationId = 0,
            string query = "",
            int pageNumber = 1)
        {
            var model = await db.Entities
                .Include(i => i.Translations)
                .Where(i =>
                    (collectionId == 0 || i.Document.CollectionId == collectionId) &&
                    (documentId == 0 || i.DocumentId == documentId) &&
                    (keywordId == 0 || i.Keywords.Any(k => k.Id == keywordId)) &&
                    (classificationId == 0 || i.ClassificationId == classificationId))
                .Where(i => query == "" || i.ImageCode.Contains(query) || i.Translations.Any(t => t.Title.Contains(query)))
                .OrderBy(i => i.Id)
                .Select(img => new TranslatedViewModel<Image, ImageTranslation>
                {
                    Entity = img
                })
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.Query = query;
            ViewBag.CollectionId = collectionId;
            ViewBag.DocumentId = documentId;
            ViewBag.ClassificationId = classificationId;
            ViewBag.KeywordId = keywordId;


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var image = await db.GetByIdAsync(id);

            if (image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            image.Translations = image.Translations.ToList();

            return View(new ImageViewModel(image));
        }

        #region Create
        public async Task<ActionResult> Create(int documentId = 0, int keywordId = 0, int classificationId = 0)
        {
            var image = new Image();

            if (db.Set<Document>().Any(d => d.Id == documentId))
            {
                image.DocumentId = documentId;
                image.ImageCode = CodeGenerator.SuggestImageCode(documentId);
            }

            if (keywordId != 0)
            {
                image.Keywords = await db.Set<Keyword>().Where(k => k.Id == keywordId).ToListAsync();
            }

            if (classificationId != 0)
            {
                image.ClassificationId = classificationId;
            }

            image.Translations.Add(new ImageTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            var model = new ImageEditViewModel(image);
            model.PopulateDropDownLists(db.Set<Document>(), db.Set<Classification>(), db.Set<Keyword>());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImageEditViewModel model)
        {
            if (DoesCodeAlreadyExist(model.Image))
            {
                ModelState.AddModelError("Image.ImageCode", ImageStrings.CodeAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var path = Server.MapPath("~/Public/Images/");

                    FileUploadHelper.GenerateVersions(model.ImageUpload.InputStream, path + fileName);

                    model.Image.ImageUrl = fileName;
                }

                model.Image.Keywords =
                    db.Set<Keyword>()
                       .Where(kw => model.KeywordIds.Contains(kw.Id))
                       .ToList();

                db.Add(model.Image);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            model.PopulateDropDownLists(db.Set<Document>(), db.Set<Classification>(), db.Set<Keyword>());

            return View(model);
        }


        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = await db.GetByIdAsync(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            var model = new ImageEditViewModel(image);

            model.PopulateDropDownLists(db.Set<Document>(), db.Set<Classification>(), db.Set<Keyword>());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ImageEditViewModel model)
        {
            if (DoesCodeAlreadyExist(model.Image))
            {
                ModelState.AddModelError("Image.ImageCode", ImageStrings.CodeAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                // "Force-load" the image and the keywords.
                await db.ForceLoadAsync(model.Image, i => i.Keywords);

                db.Update(model.Image);

                model.Image.Keywords = db.Set<Keyword>()
                                         .Where(k => model.KeywordIds.Contains(k.Id)).ToList();

                foreach (var t in model.Image.Translations)
                {
                    db.UpdateTranslation(t);
                }

                if (model.ImageUpload != null)
                {
                    var fileName =
                        db.GetValueFromDb(model.Image, i => i.ImageUrl) ??
                        Guid.NewGuid().ToString();

                    var path = Server.MapPath("~/Public/Images/");
                    Directory.CreateDirectory(path);

                    FileUploadHelper.GenerateVersions(model.ImageUpload.InputStream, path + fileName);

                    model.Image.ImageUrl = fileName;
                }
                else
                {
                    db.ExcludeFromUpdate(model.Image, i => new { i.ImageUrl });
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            model.PopulateDropDownLists(db.Set<Document>(), db.Set<Classification>(), db.Set<Keyword>());

            return View(model);
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = await db.GetByIdAsync(id);

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
            var image = await db.GetByIdAsync(id);

            db.Remove(image);

            if (image.ImageUrl != null)
            {
                var fullPath = Server.MapPath("~/Public/Images/" + image.ImageUrl);

                // Remove file from the disk.
                if (System.IO.File.Exists(fullPath + "_thumb.jpg"))
                {
                    System.IO.File.Delete(fullPath + "_thumb.jpg");
                }

                if (System.IO.File.Exists(fullPath + "_large.jpg"))
                {
                    System.IO.File.Delete(fullPath + "_large.jpg");
                }
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Helper Actions
        public ActionResult SuggestCode(int? documentId)
        {
            if (documentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var d = db.Set<Document>().FirstOrDefault(doc => doc.Id == documentId.Value);

            if (d == null)
            {
                return HttpNotFound();
            }

            return Json(CodeGenerator.SuggestImageCode(d.Id), JsonRequestBehavior.AllowGet);
        }

        private bool DoesCodeAlreadyExist(Image image)
        {
            return db.Entities
                .Any(d => d.ImageCode == image.ImageCode && d.Id != image.Id);
        }
        #endregion

        #region Translation Actions
        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = await db.GetByIdAsync(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            var model = new ImageTranslation
            {
                ImageId = image.Id
            };

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(image.Translations.Select(t => t.LanguageCode));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(ImageTranslation translation)
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