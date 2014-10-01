using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

// TODO (@redroserade): Redo this.

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class DigitalPhotographsController : ArchiveControllerBase
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/DigitalPhotographs/Create
        public ActionResult Create(int? specimenId)
        {
            if (specimenId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var specimen = db.Specimens.Find(specimenId);

            if (specimen == null)
            {
                return HttpNotFound();
            }

            var model = new SpecimenPhotoUploadModel
            {
                SpecimenId = specimenId.Value
            };

            return View(model);
        }

        // POST: BackOffice/DigitalPhotographs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpecimenPhotoUploadModel model)
        {
            if (ModelState.IsValid)
            {
                var returnModel = new DigitalPhotographPostUploadModel();

                foreach (var photo in model.Photos)
                {
                    var fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    var dPhoto = new DigitalPhotograph
                    {
                        SpecimenId = model.SpecimenId,
                        FileName = fileName
                    };

                    // Save the file and the smaller versions.
                    var completePath = Server.MapPath("~/App_Data/Uploads/Photos/");
                    Directory.CreateDirectory(completePath);
                    photo.SaveAs(completePath + fileName);
                    FileUploadHelper.GenerateVersions(completePath + fileName);

                    returnModel.UploadedItems.Add(new DigitalPhotographUploadItem
                        {
                            DigitalPhotograph = dPhoto
                        });

                    db.DigitalPhotographs.Add(dPhoto);
                }

                await db.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                {
                    return PartialView("PostUpload", returnModel);
                }

                return View("PostUpload", returnModel);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostUpload(DigitalPhotographPostUploadModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model.UploadedItems)
                {
                    var photo =
                        await db.DigitalPhotographs.FindAsync(item.DigitalPhotograph.Id);

                    if (!item.Save)
                    {
                        // Remove the picture and its files.
                        db.DigitalPhotographs.Remove(photo);

                        var fullPath = Server.MapPath("~/App_Data/Uploads/Photos/" + photo.FileName);

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
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Details", "Specimens", new { id = model.UploadedItems[0].DigitalPhotograph.SpecimenId });
            }

            return View(model);
        }

        // GET: BackOffice/DigitalPhotographs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DigitalPhotograph digitalPhotograph = await db.DigitalPhotographs.FindAsync(id);
            if (digitalPhotograph == null)
            {
                return HttpNotFound();
            }
            return View(digitalPhotograph);
        }

        // POST: BackOffice/DigitalPhotographs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DigitalPhotograph digitalPhotograph = await db.DigitalPhotographs.FindAsync(id);

            var fullPath = Server.MapPath("~/App_Data/Uploads/Photos/" + digitalPhotograph.FileName);

            // Remove file from the disk.
            if (System.IO.File.Exists(fullPath + "_thumb.jpg"))
            {
                System.IO.File.Delete(fullPath + "_thumb.jpg");
            }

            if (System.IO.File.Exists(fullPath + "_large.jpg"))
            {
                System.IO.File.Delete(fullPath + "_large.jpg");
            }

            db.DigitalPhotographs.Remove(digitalPhotograph);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Specimens", new { id = digitalPhotograph.SpecimenId });
        }

        [OutputCache(Duration = int.MaxValue, VaryByParam = ("id;size;download"))]
        public async Task<ActionResult> GetImage(int? id, string size = "large", bool download = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var img = await db.DigitalPhotographs.FindAsync(id);

            if (img == null)
            {
                return HttpNotFound();
            }

            if (download)
            {
                return File(Server.MapPath("~/App_Data/Uploads/Photos/" + img.FileName), img.MimeType, img.FileName);
            }

            var fileName = Path.GetFileNameWithoutExtension(img.FileName);

            switch (size.ToLower())
            {
                case "thumb":
                    fileName += "_thumb.jpg";
                    break;

                case "large":
                default:
                    fileName += "_large.jpg";
                    break;
            }

            return File(Server.MapPath("~/App_Data/Uploads/Photos/" + fileName), "image/jpeg");
        }

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