using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class SpecimensController : ArchiveController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/Specimens
        public ActionResult Index(int? imageId, int pageNumber = 1)
        {
            if (imageId.HasValue)
            {
                return View(_db.Specimens
                           .Where(s => s.ImageId == imageId)
                           .OrderBy(s => new { s.ImageId, s.Id })
                           .ToPagedList(pageNumber, 10));
            }

            return View(_db.Specimens
                           .OrderBy(s => new { s.ImageId, s.Id })
                           .ToPagedList(pageNumber, 10));
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

            specimen.DigitalPhotographs = specimen.DigitalPhotographs.ToList();

            return View(specimen);
        }

        // GET: BackOffice/Specimens/Create
        public async Task<ActionResult> Create(int imageId = 0)
        {
            var s = new Specimen();

            if (imageId > 0)
            {
                var image = await _db.Images.FindAsync(imageId);

                if (image != null)
                {
                    s.ImageId = image.Id;

                    var doc = image.Document;
                    var collection = doc.Collection;

                    s.ReferenceCode = CodeGenerator.SuggestSpecimenCode(imageId);
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

            return View(GenerateViewModel(s));
        }

        // POST: BackOffice/Specimens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Specimen specimen)
        {
            if (ModelState.IsValid)
            {
                _db.Specimens.Add(specimen);

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(specimen));
        }

        // GET: BackOffice/Specimens/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

            return View(GenerateViewModel(specimen));
        }

        // POST: BackOffice/Specimens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Specimen specimen)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(specimen).State = EntityState.Modified;

                foreach (var t in specimen.Translations)
                {
                    _db.Entry(t).State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(specimen));
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

        //[OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 3600)]
        //public async Task<ActionResult> GetPicture(int? id, string size = "Large")
        //{
        //    if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

        //    var p = await _db.DigitalPhotographs.FindAsync(id);

        //    if (p == null) { return HttpNotFound(); }

        //    if (size == "Original")
        //    {
        //        var specimen = p.Specimen;

        //        var fileName = p.OriginalFileName;

        //        return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Original", p.FileName), p.MimeType, fileName);
        //    }

        //    // Caching. Basically, we check if the browser has cached the picture (present if "If-Modified-Since" is in the headers).
        //    // If so, we check if the image was modified since then, and we'll return it.
        //    // From http://weblogs.asp.net/jeff/archive/2009/07/01/304-your-images-from-a-database.aspx

        //    // TODO?: There may be some implications with this. Need to check them better.

        //    if (!String.IsNullOrEmpty(Request.Headers["If-None-Match"]))
        //    {
        //        var eTag = Request.Headers["If-None-Match"];

        //        if (eTag == p.FileName)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.NotModified);
        //        }
        //    }

        //    Response.Cache.SetETag(p.FileName);

        //    //if (!String.IsNullOrEmpty(Request.Headers["If-Modified-Since"]))
        //    //{
        //    //    CultureInfo provider = CultureInfo.InvariantCulture;
        //    //    var lastMod = DateTime.ParseExact(Request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();

        //    //    // if (lastMod == p.LastModified.AddMilliseconds(-p.LastModified.Millisecond))
        //    //    if (lastMod.CompareTo(p.LastModified) > 0)
        //    //    {
        //    //        return new HttpStatusCodeResult(HttpStatusCode.NotModified);
        //    //    }
        //    //}

        //    switch (size)
        //    {
        //        case "Thumb":
        //            return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Thumb", Path.GetFileNameWithoutExtension(p.FileName) + ".jpg"), "image/jpeg");

        //        case "Large":
        //        default:
        //            return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", Path.GetFileNameWithoutExtension(p.FileName) + ".jpg"), "image/jpeg");
        //    }
        //}

        public async Task<ActionResult> ListPhotos(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var s = await _db.Specimens.FindAsync(id);

            if (s == null) { return HttpNotFound(); }

            ViewBag.Id = s.Id;

            return View(s.DigitalPhotographs.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private SpecimenEditViewModel GenerateViewModel(Specimen specimen)
        {
            var model = new SpecimenEditViewModel();

            model.AvailableImages.Add(new SelectListItem
                {
                    Value = "",
                    Text = UiPrompts.ChooseOne,
                    Selected = true
                });

            model.AvailableImages.AddRange(
                _db.Images
                   .OrderBy(i => i.Id)
                   .Select(i => new SelectListItem
                   {
                       Value = i.Id.ToString(),
                       Text = i.ImageCode,
                       Selected = specimen.ImageId == i.Id
                   }));

            model.AvailableProcesses.Add(new SelectListItem
            {
                Value = "",
                Text = UiPrompts.ChooseOne,
                Selected = true
            });

            model.AvailableProcesses.AddRange(
                _db.ProcessTranslations
                   .Where(pt => pt.LanguageCode == LanguageDefinitions.DefaultLanguage)
                   .OrderBy(pt => pt.ProcessId)
                   .Select(pt => new SelectListItem
                {
                    Value = pt.ProcessId.ToString(),
                    Text = pt.Value,
                    Selected = specimen.ProcessId == pt.ProcessId
                }));

            model.AvailableFormats.Add(new SelectListItem
            {
                Value = "",
                Text = UiPrompts.ChooseOne,
                Selected = true
            });

            model.AvailableFormats.AddRange(_db.Formats
                .OrderBy(f => f.Id)
                .Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = f.FormatDescription,
                    Selected = specimen.FormatId == f.Id
                }));

            model.Specimen = specimen;

            return model;
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AssociateDigitalPhoto(SpecimenPhotoUploadModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var photosToAdd = new List<DigitalPhotograph>();

        //        foreach (var item in model.Items.Where(i => i.IsToConsider))
        //        {
        //            // Get the file for this photo item. I'm only doing this to be sure that
        //            // they match.
        //            var file = model.Photos.First(p => p.FileName == item.OriginalFileName);

        //            // Use a Guid as the new file name. It's safer and very unlikely
        //            // to generate conflicts.
        //            var newName = Guid.NewGuid().ToString();

        //            photosToAdd.Add(new DigitalPhotograph
        //            {
        //                SpecimenId = model.SpecimenId,
        //                CopyrightInfo = item.CopyrightInfo,
        //                ScanDate = item.ScanDate,
        //                Process = item.Process,
        //                OriginalFileName = item.OriginalFileName,
        //                IsVisible = item.IsVisible,
        //                FileName = newName + Path.GetExtension(file.FileName),
        //                MimeType = file.ContentType
        //            });

        //            // Prepare to resize the pictures.
        //            // We'll scale them proportionally to a maximum of 1024x768.
        //            // We also can't dispose of the source image just yet.
        //            ImageJob j = new ImageJob
        //            {
        //                Instructions = new Instructions
        //                {
        //                    Width = 1024, // TODO To define later.
        //                    Height = 768,
        //                    Mode = FitMode.Max
        //                },
        //                Source = file.InputStream,
        //                Dest = Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", newName + ".jpg"),
        //                CreateParentDirectory = true,
        //                ResetSourceStream = true,
        //                DisposeSourceObject = false
        //            };
        //            // Save the "Large" image.
        //            ImageBuilder.Current.Build(j);

        //            // Re-use the object, but change the dimensions to
        //            // the "Thumb" size.
        //            j.Instructions.Width = 200;
        //            j.Instructions.Height = 200;
        //            j.DisposeSourceObject = false;
        //            j.Dest = Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Thumb", newName + ".jpg");

        //            ImageBuilder.Current.Build(j);

        //            var path = Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Original", newName + Path.GetExtension(file.FileName));

        //            Directory.CreateDirectory(Path.GetDirectoryName(path));

        //            // Then, save the "Original" picture.
        //            file.SaveAs(path);

        //            // ...and dispose of it, freeing memory.
        //            file.InputStream.Dispose();
        //        }

        //        // Save the data to the db.
        //        _db.DigitalPhotographs.AddRange(photosToAdd);

        //        await _db.SaveChangesAsync();

        //        return RedirectToAction("Index");
        //    }

        //    if (Request.IsAjaxRequest()) return PartialView(model);

        //    return View(model);
        //}

        public async Task<ActionResult> SuggestCode(int? imageId)
        {
            if (imageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var i = await _db.Images.FindAsync(imageId);

            if (i == null)
            {
                return HttpNotFound();
            }

            return Json(CodeGenerator.SuggestSpecimenCode(i.Id), JsonRequestBehavior.AllowGet);
        }
    }
}