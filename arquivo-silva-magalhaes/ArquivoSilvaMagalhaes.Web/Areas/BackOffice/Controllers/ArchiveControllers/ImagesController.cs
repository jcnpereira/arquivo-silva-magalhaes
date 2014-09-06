using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ImagesController : ArchiveControllerBase
    {
        private ITranslateableRepository<Image, ImageTranslation> db;

        public ImagesController()
            : this(new TranslateableGenericRepository<Image, ImageTranslation>()) { }

        public ImagesController(ITranslateableRepository<Image, ImageTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Image
        public async Task<ActionResult> Index(int pageNumber = 1, int documentId = 0)
        {
            var query = await db.QueryAsync(i => documentId == 0 || i.DocumentId == documentId);

            return View(query
                .Select(i => new TranslatedViewModel<Image, ImageTranslation>(i))
                .ToPagedList(pageNumber, 10));
        }

        public async Task<ActionResult> Details(int id)
        {
            var image = await db.GetByIdAsync(id);

            if (image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            image.Translations = image.Translations.ToList();

            return View(image);
        }

        public ActionResult Create(int documentId = 0)
        {
            var image = new Image();

            if (db.Set<Document>().Any(d => d.Id == documentId))
            {
                image.DocumentId = documentId;
                image.ImageCode = CodeGenerator.SuggestImageCode(documentId);
            }

            image.Translations.Add(new ImageTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            var model = new ImageEditViewModel(image, true);
            model.PopulateDropDownLists(db.Set<Document>(), db.Set<Classification>(), db.Set<Keyword>());

            return View(model);
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
                    db.Set<Keyword>()
                       .Where(kw => model.KeywordIds.Contains(kw.Id))
                       .ToList();

                db.Add(model.Image);
                await db.SaveChangesAsync();

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
            var image = await db.GetByIdAsync(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            var model = new ImageEditViewModel(image, true);

            model.PopulateDropDownLists(db.Set<Document>(), db.Set<Classification>(), db.Set<Keyword>());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ImageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Update(model.Image);

                // "Force-load" the image and the keywords.
                await db.ForceLoadAsync(model.Image, i => i.Keywords);

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
                        Guid.NewGuid().ToString() + "_" + Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);

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
            await db.RemoveByIdAsync(id);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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