using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Diagnostics;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using System.IO;
using System.Drawing;
using ArquivoSilvaMagalhaes.Utilitites;
using System.Drawing.Imaging;
using ImageResizer;
using System.Web.UI;
using System.Globalization;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Resources;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class SpecimensController : BackOfficeController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/Specimens
        public async Task<ActionResult> Index(int? imageId)
        {
            return View(await _db.Specimens.ToListAsync());
        }

        // GET: BackOffice/Specimens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await _db.Specimens.FindAsync(id);
            if (specimen == null)
            {
                return HttpNotFound();
            }
            return View(specimen);
        }

        private SpecimenEditViewModel PopulateDropDownLists(Specimen specimen)
        {
            var model = new SpecimenEditViewModel();

            model.AvailableImages = _db.Images
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.ImageCode,
                    Selected = specimen.ImageId == i.Id
                })
                .ToList();

            model.AvailableProcesses = _db.Processes
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Translations.FirstOrDefault(pt => pt.LanguageCode == LanguageDefinitions.DefaultLanguage).Value,
                    Selected = specimen.ProcessId == p.Id
                })
                .ToList();


            model.AvailableFormats = _db.Formats
                .Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = f.FormatDescription,
                    Selected = specimen.FormatId == f.Id
                })
                .ToList();

            model.AvailableLanguages = specimen.Translations
                .Where(t => !LanguageDefinitions.Languages.Contains(t.LanguageCode))
                .Select(t => t.LanguageCode)
                .ToList();

            return model;
        }

        // GET: BackOffice/Specimens/Create
        public ActionResult Create(int? imageId)
        {
            var s = new Specimen();

            if (imageId != null)
            {
                var image = _db.Images.Find(imageId);

                if (image != null)
                {
                    s.ImageId = image.Id;
                    
                    var doc = image.Document;
                    var collection = doc.Collection;

                    s.ReferenceCode = 
                        String.Format("{0}/{1}/{2}-", 
                                      doc.CatalogCode, 
                                      collection.CatalogCode, 
                                      image.ImageCode
                        );
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.Specimen__UnknownImage);
                }
            }

            s.Translations.Add(new SpecimenTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

            var model = new Tuple<Specimen, SpecimenEditViewModel>(s, PopulateDropDownLists(s));

            return View(model);
        }

        // POST: BackOffice/Specimens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Specimen s)
        {
            if (ModelState.IsValid)
            {
                _db.Specimens.Add(s);

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var model = new Tuple<Specimen, SpecimenEditViewModel>(s, PopulateDropDownLists(s));

            return View(model);

            //var t = s.Translations.First(tr => tr.LanguageCode == LanguageDefinitions.DefaultLanguage);

            //return View(new SpecimenEditViewModel
            //    {
            //        Id = s.Id,
            //        CatalogCode = s.CatalogCode,
            //        AuthorCatalogationCode = s.AuthorCatalogationCode,
            //        HasMarksOrStamps = s.HasMarksOrStamps,
            //        Indexation = s.Indexation,
            //        Notes = s.Notes,
            //        ProcessId = s.ProcessId,
            //        Translations = new List<SpecimenTranslationEditViewModel>
            //        {
            //            new SpecimenTranslationEditViewModel
            //            {
            //                SpecimenId = s.Id,
            //                LanguageCode = t.LanguageCode,
            //                Title = t.Title,
            //                Topic = t.Topic,
            //                Description = t.Description,
            //                DetailedStateDescription = t.DetailedStateDescription,
            //                InterventionDescription = t.InterventionDescription,
            //                Publication = t.Publication
            //            }
            //        }
            //    });
        }

        // GET: BackOffice/Specimens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen s = await _db.Specimens.FindAsync(id);
            if (s == null)
            {
                return HttpNotFound();
            }

            var model = new Tuple<Specimen, SpecimenEditViewModel>(s, PopulateDropDownLists(s));
            return View(model);

            //var t = s.Translations.First(tr => tr.LanguageCode == LanguageDefinitions.DefaultLanguage);

            //return View(new SpecimenEditViewModel
            //    {
            //        Id = s.Id,
            //        CatalogCode = s.CatalogCode,
            //        AuthorCatalogationCode = s.AuthorCatalogationCode,
            //        HasMarksOrStamps = s.HasMarksOrStamps,
            //        Indexation = s.Indexation,
            //        Notes = s.Notes,
            //        FormatId = s.FormatId,
            //        ProcessId = s.ProcessId,
            //        Translations = new List<SpecimenTranslationEditViewModel>
            //        {
            //            new SpecimenTranslationEditViewModel
            //            {
            //                SpecimenId = s.Id,
            //                LanguageCode = t.LanguageCode,
            //                Title = t.Title,
            //                Topic = t.Topic,
            //                Description = t.Description,
            //                DetailedStateDescription = t.DetailedStateDescription,
            //                InterventionDescription = t.InterventionDescription,
            //                Publication = t.Publication
            //            }
            //        }
            //    });
        }

        // POST: BackOffice/Specimens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Specimen s)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(s).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var model = new Tuple<Specimen, SpecimenEditViewModel>(s, PopulateDropDownLists(s));
            return View(model);

            //return View(specimen);
        }

        // GET: BackOffice/Specimens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await _db.Specimens.FindAsync(id);
            if (specimen == null)
            {
                return HttpNotFound();
            }
            return View(specimen);
        }

        // POST: BackOffice/Specimens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Specimen specimen = await _db.Specimens.FindAsync(id);
            _db.Specimens.Remove(specimen);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        /// <summary>
        /// To associate a digital photo with a specimen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> AssociateDigitalPhoto(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var specimen = await _db.Specimens.FindAsync(id);

            if (specimen == null) { return HttpNotFound(); }

            return View(new SpecimenPhotoUploadModel { SpecimenId = id.Value });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssociateDigitalPhoto(SpecimenPhotoUploadModel model)
        {
            if (ModelState.IsValid)
            {
                var photosToAdd = new List<DigitalPhotograph>();

                foreach (var item in model.Items.Where(i => i.IsToConsider))
                {
                    // Get the file for this photo item. I'm only doing this to be sure that
                    // they match.
                    var file = model.Photos.First(p => p.FileName == item.OriginalFileName);

                    // Use a Guid as the new file name. It's safer and very unlikely
                    // to generate conflicts.
                    var newName = Guid.NewGuid().ToString();

                    photosToAdd.Add(new DigitalPhotograph
                    {
                        SpecimenId = model.SpecimenId,
                        CopyrightInfo = item.CopyrightInfo,
                        ScanDate = item.ScanDate,
                        Process = item.Process,
                        OriginalFileName = item.OriginalFileName,
                        IsVisible = item.IsVisible,
                        FileName = newName + Path.GetExtension(file.FileName),
                        MimeType = file.ContentType
                    });

                    // Prepare to resize the pictures.
                    // We'll scale them proportionally to a maximum of 1024x768.
                    // We also can't dispose of the source image just yet.
                    ImageJob j = new ImageJob
                    {
                        Instructions = new Instructions
                        {
                            Width = 1024, // TODO To define later.
                            Height = 768,
                            Mode = FitMode.Max
                        },
                        Source = file.InputStream,
                        Dest = Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", newName + ".jpg"),
                        CreateParentDirectory = true,
                        ResetSourceStream = true,
                        DisposeSourceObject = false
                    };
                    // Save the "Large" image.
                    ImageBuilder.Current.Build(j);

                    // Re-use the object, but change the dimensions to
                    // the "Thumb" size.
                    j.Instructions.Width = 200;
                    j.Instructions.Height = 200;
                    j.DisposeSourceObject = false;
                    j.Dest = Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Thumb", newName + ".jpg");

                    ImageBuilder.Current.Build(j);

                    var path = Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Original", newName + Path.GetExtension(file.FileName));

                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                    // Then, save the "Original" picture.
                    file.SaveAs(path);

                    // ...and dispose of it, freeing memory.
                    file.InputStream.Dispose();
                }

                // Save the data to the db.
                _db.DigitalPhotographs.AddRange(photosToAdd);

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest()) return PartialView(model);

            return View(model);
        }

        public async Task<ActionResult> ListPhotos(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var s = await _db.Specimens.FindAsync(id);

            if (s == null) { return HttpNotFound(); }

            ViewBag.Id = s.Id;

            return View(s.DigitalPhotographs.ToList());
        }

        // TODO: Permitir o download da foto original, com OU o nome original, ou um nome semântico, baseado no autor e no documento.

        [OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 3600)]
        public async Task<ActionResult> GetPicture(int? id, string size = "Large")
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await _db.DigitalPhotographs.FindAsync(id);

            if (p == null) { return HttpNotFound(); }

            if (size == "Original")
            {
                var specimen = p.Specimen;

                var fileName = p.OriginalFileName;

                return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Original", p.FileName), p.MimeType, fileName);
            }

            // Caching. Basically, we check if the browser has cached the picture (present if "If-Modified-Since" is in the headers).
            // If so, we check if the image was modified since then, and we'll return it.
            // From http://weblogs.asp.net/jeff/archive/2009/07/01/304-your-images-from-a-database.aspx

            // TODO?: There may be some implications with this. Need to check them better.

            if (!String.IsNullOrEmpty(Request.Headers["If-None-Match"]))
            {
                var eTag = Request.Headers["If-None-Match"];

                if (eTag == p.FileName)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotModified);
                }
            }

            Response.Cache.SetETag(p.FileName);

            //if (!String.IsNullOrEmpty(Request.Headers["If-Modified-Since"]))
            //{
            //    CultureInfo provider = CultureInfo.InvariantCulture;
            //    var lastMod = DateTime.ParseExact(Request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();




            //    // if (lastMod == p.LastModified.AddMilliseconds(-p.LastModified.Millisecond))
            //    if (lastMod.CompareTo(p.LastModified) > 0)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.NotModified);
            //    }
            //}

            switch (size)
            {
                case "Thumb":
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Thumb", Path.GetFileNameWithoutExtension(p.FileName) + ".jpg"), "image/jpeg");
                case "Large":
                default:
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", Path.GetFileNameWithoutExtension(p.FileName) + ".jpg"), "image/jpeg");
            }
        }

        public async Task<ActionResult> DeletePhoto(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await _db.DigitalPhotographs.FindAsync(id);

            if (p == null) { return new HttpStatusCodeResult(HttpStatusCode.NotFound); }

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeletePhoto")]
        public async Task<ActionResult> DeletePhotoConfirmed(int? id, string redirectTo = "ListPhotos")
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await _db.DigitalPhotographs.FindAsync(id);

            if (p == null) { return new HttpStatusCodeResult(HttpStatusCode.NotFound); }

            _db.DigitalPhotographs.Remove(p);

            // TODO: Remove all files from the disk.

            await _db.SaveChangesAsync();

            return RedirectToAction(redirectTo, new { id = p.SpecimenId });
        }

        public async Task<ActionResult> EditPhotoDetails(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await _db.DigitalPhotographs.FindAsync(id);

            if (p == null) { return new HttpStatusCodeResult(HttpStatusCode.NotFound); }

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPhotoDetails([Bind(Exclude = "FileName,OriginalFileName,Encoding,LastModified")] DigitalPhotograph photo)
        {
            if (ModelState.IsValid)
            {
                var p = await _db.DigitalPhotographs.FindAsync(photo.Id);

                p.IsVisible = photo.IsVisible;
                p.ScanDate = photo.ScanDate;
                p.CopyrightInfo = photo.CopyrightInfo;
                p.Process = photo.Process;

                p.LastModified = DateTime.Now;
                _db.Entry(p).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("ListPhotos", new { id = photo.SpecimenId });
            }

            return View(photo);
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
