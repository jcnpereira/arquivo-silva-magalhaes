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
using System.Data.Entity;
using System.Diagnostics;

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
            IQueryable<Image> images;

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

            var model = (from i in images
                         join it in _db.ImageTranslations on i.Id equals it.ImageId
                         where it.LanguageCode == LanguageDefinitions.DefaultLanguage && it.Title.StartsWith("S")
                         select i)
                        .ToPagedList(pageNumber, 10);


            //var model = images
            //    // .Where(i => i.Title.StartsWith("S"))
            //    .OrderBy(i => i.Id)
            //    .ToList()
            //    .Where(i => i.Title.StartsWith("S"))
            //    .ToPagedList(pageNumber, 10);

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
                    PopulateDropDownLists(documentId: documentId);
                    return View(new Image
                        {
                            DocumentId = documentId.Value
                        });
                }
            }
            else
            {
                PopulateDropDownLists(documentId: documentId);
                return View();
            }

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

                // image.Title = "Título, em português.";

                try
                {
                    _db.Images.Add(image);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                return RedirectToAction("Index");
            }

            PopulateDropDownLists(documentId: image.DocumentId, keywordIds: keywordIds);

            return View(image);
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