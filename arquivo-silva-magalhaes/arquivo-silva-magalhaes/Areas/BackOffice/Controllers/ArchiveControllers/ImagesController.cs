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

        public ActionResult Edit(int? imageId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image i)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(i).State = EntityState.Modified;
                _db.SaveChanges();
            }

            return View(i);
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