using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class ImagesController : BackOfficeController
    {
        ArchiveDataContext _db = new ArchiveDataContext();



        // GET: BackOffice/Image
        public ActionResult Index(
            int pageNumber = 1,
            int documentId = 0,
            string query = "")
        {
            IEnumerable<Image> images;

            if (documentId > 0)
            {
                images = _db.Images.Where(i => i.DocumentId == documentId);
            }
            //else if (!String.IsNullOrEmpty(query))
            //{
            //    images = _db.Images.Where(i => i.ImageCode == query);
            //}
            else
            {
                images = _db.Images;
            }

            var model = images
                .OrderBy(i => i.Id)
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
            if (documentId != null && _db.Documents.Find(documentId) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.Image__UnknownDocument);
            }

            PopulateDropDownLists(documentId: documentId);

            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Image image, int[] keywordIds)
        {
            // Test for conflicts in the image's code.
            if (_db.Images.Any(i => i.ImageCode == image.ImageCode))
            {
                ModelState.AddModelError(String.Empty, ErrorStrings.Image__CodeAlreadyExists);
                PopulateDropDownLists(keywordIds: keywordIds);
                return View(image);
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

            return View(image);
        }

        /// <summary>
        /// Algorithm by @jcnpereira. Implementation by @afecarvalho.
        /// </summary>
        /// <returns></returns>
        private string GenerateNewImageCode()
        {
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
        private void PopulateDropDownLists(int[] keywordIds = null, int? documentId = null)
        {
            keywordIds = keywordIds ?? new int[] { };
            documentId = documentId ?? 0;

            if (documentId == 0)
            {
                ViewBag.DocumentIds = _db.Documents
                    .OrderBy(d => d.Id)
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Title ?? "Ainda não implementado. Id: " + d.Id, // TODO
                        Selected = documentId == d.Id
                    })
                .ToList();
            }

            ViewBag.KeywordIds = _db.KeywordTranslations
                .Where(k => k.LanguageCode == LanguageDefinitions.DefaultLanguage)
                .OrderBy(k => k.KeywordId)
                .Select(k => new SelectListItem
                {
                    Value = k.KeywordId.ToString(),
                    Text = k.Value,
                    Selected = keywordIds.Contains(k.KeywordId)
                })
                .ToList();
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