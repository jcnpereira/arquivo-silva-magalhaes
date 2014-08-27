using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                        ScanDate = DateTime.Now,
                        FileName = fileName,
                        MimeType = photo.ContentType
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
                    }
                    else
                    {
                        // Update the selected fields.
                        photo.Notes = item.DigitalPhotograph.Notes;
                        photo.ScanDate = item.DigitalPhotograph.ScanDate;

                        db.Entry(photo).State = EntityState.Modified;
                    }
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }


        // GET: BackOffice/DigitalPhotographs/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.SpecimenId = new SelectList(db.Specimens, "Id", "AuthorCatalogationCode", digitalPhotograph.SpecimenId);
            return View(digitalPhotograph);
        }

        // POST: BackOffice/DigitalPhotographs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SpecimenId,ScanDate,Notes")] DigitalPhotograph digitalPhotograph)
        {
            if (ModelState.IsValid)
            {
                var dp = await db.DigitalPhotographs.FindAsync(digitalPhotograph.Id);

                dp.Notes = digitalPhotograph.Notes;
                dp.ScanDate = digitalPhotograph.ScanDate;

                db.Entry(dp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SpecimenId = new SelectList(db.Specimens, "Id", "AuthorCatalogationCode", digitalPhotograph.SpecimenId);
            return View(digitalPhotograph);
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
            db.DigitalPhotographs.Remove(digitalPhotograph);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [OutputCache(Duration = int.MaxValue, VaryByParam=("id;size;download"))]
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
