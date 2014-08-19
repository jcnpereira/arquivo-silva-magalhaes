using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
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

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ImagesController : ArchiveController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/Image
        public async Task<ActionResult> Index(
            int pageNumber = 1,
            int documentId = 0)
        {
            var query = _db.ImageTranslations.Include(it => it.Image);

            if (await _db.Documents.FindAsync(documentId) != null)
            {
                query = query.Where(i => i.Image.DocumentId == documentId);
            }

            return View(await Task.Run(() => 
                query.OrderBy(i => i.ImageId)
                     .ToPagedList(pageNumber, 10)));
        }

        public ActionResult Details(int id)
        {
            var image = _db.Images.Find(id);

            if (image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(image);
        }

        public ActionResult Create(int? documentId)
        {
            var image = new Image();

            image.Translations.Add(new ImageTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            });

            // Check for an existing document. If there is
            // a document available, no drop-down lists are
            // created. If an id was supplied and no document
            // exists with such id, an exception is raised.
            if (documentId != null)
            {
                var document = _db.Documents.Find(documentId);

                if (document == null)
                {
                    return new HttpStatusCodeResult(
                        HttpStatusCode.BadRequest,
                        ErrorStrings.Image__UnknownDocument
                    );
                }
                else
                {
                    image.ImageCode = CodeGenerator.SuggestImageCode(documentId.Value);
                    image.DocumentId = document.Id;
                }
            }

            return View(GenerateViewModel(image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Image image, int[] keywordIds)
        {
            if (ModelState.IsValid)
            {
                keywordIds = keywordIds ?? new int[] { };

                var keywords = _db.Keywords.Where(kw => keywordIds.Contains(kw.Id));

                foreach (var kw in keywords)
                {
                    image.Keywords.Add(kw);
                }
                _db.Images.Add(image);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(image));
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = await _db.Images.FindAsync(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            return View(GenerateViewModel(image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Image image, int[] keywordIds)
        {
            if (ModelState.IsValid)
            {
                // "Force-load" the collection and the authors.
                _db.Images.Attach(image);
                _db.Entry(image).Collection(i => i.Keywords).Load();
                // The forced-loading was required so that the author list can be updated.
                image.Keywords = _db.Keywords.Where(k => keywordIds.Contains(k.Id)).ToList();

                _db.Entry(image).State = EntityState.Modified;

                foreach (var t in image.Translations)
                {
                    _db.Entry(t).State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(image));
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = await _db.Images.FindAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: /BackOffice/Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Image image = await _db.Images.FindAsync(id);

            _db.Images.Remove(image);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SuggestCode(int? documentId)
        {
            if (documentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var d = await _db.Documents.FindAsync(documentId);

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
                _db.Documents
                .OrderBy(d => d.Id)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.CatalogCode + " - " + d.Title,
                    Selected = d.Id == image.DocumentId
                }));

            var keywordIds = image.Keywords.Select(k => k.Id).ToList();

            model.AvailableKeywords = _db.KeywordTranslations
                .Where(kt => kt.LanguageCode == LanguageDefinitions.DefaultLanguage)
                .OrderBy(kt => kt.KeywordId)
                .Select(kt => new SelectListItem
                {
                    Value = kt.KeywordId.ToString(),
                    Text = kt.Value,
                    Selected = keywordIds.Contains(kt.KeywordId)
                })
                .ToList();

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