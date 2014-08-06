using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ImagesController : BackOfficeController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/Image
        public ActionResult Index(
            int pageNumber = 1,
            int documentId = 0,
            string query = "")
        {
            IQueryable<Image> images;

            if (documentId > 0)
            {
                images = _db.Images.Where(i => i.DocumentId == documentId);
            }
            else
            {
                images = _db.Images;
            }

            var model = (from i in images
                         join it in _db.ImageTranslations on i.Id equals it.ImageId
                         where it.LanguageCode == LanguageDefinitions.DefaultLanguage
                         orderby i.Id ascending
                         select i)
                        .ToPagedList(pageNumber, 10);

            return View(model);
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
                    image.DocumentId = document.Id;
                }
            }

            return View(GenerateViewModel(image));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Image image, int[] keywordIds)
        {
            // Test for conflicts in the image's code.
            if (_db.Images.Any(i => i.ImageCode == image.ImageCode))
            {
                ModelState.AddModelError(String.Empty, ErrorStrings.Image__CodeAlreadyExists);
            }

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

        /// <summary>
        /// Algorithm by @jcnpereira. Implementation by @afecarvalho.
        /// </summary>
        /// <returns></returns>
        private string GenerateNewImageCode(Image i)
        {
            if (i == null)
            {
                return "";
            }

            string lastImageCode = _db.Images.Last().ImageCode;
            int codeNumeric;

            if (int.TryParse(lastImageCode, out codeNumeric))
            {
                // Number.
                return (codeNumeric + 1).ToString();
            }
            else
            {
            }

            return null;
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

            model.AvailableDocuments = _db.Documents.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Title,
                    Selected = d.Id == image.DocumentId
                })
                .ToList();

            var keywordIds = image.Keywords.Select(k => k.Id).ToList();

            model.AvailableKeywords = _db.KeywordTranslations.Select(d => new SelectListItem
                {
                    Value = d.KeywordId.ToString(),
                    Text = d.Value,
                    Selected = keywordIds.Contains(d.KeywordId)
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