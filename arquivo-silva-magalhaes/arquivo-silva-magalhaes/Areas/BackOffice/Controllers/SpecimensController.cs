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

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class SpecimensController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Specimens
        public async Task<ActionResult> Index()
        {
            return View(await db.SpecimenSet.ToListAsync());
        }

        // GET: BackOffice/Specimens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await db.SpecimenSet.FindAsync(id);
            if (specimen == null)
            {
                return HttpNotFound();
            }
            return View(specimen);
        }

        // GET: BackOffice/Specimens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Specimens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CatalogCode,AuthorCatalogationCode,HasMarksOrStamps,Indexation,Notes")] Specimen specimen)
        {
            if (ModelState.IsValid)
            {
                db.SpecimenSet.Add(specimen);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(specimen);
        }

        // GET: BackOffice/Specimens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await db.SpecimenSet.FindAsync(id);
            if (specimen == null)
            {
                return HttpNotFound();
            }
            return View(specimen);
        }

        // POST: BackOffice/Specimens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CatalogCode,AuthorCatalogationCode,HasMarksOrStamps,Indexation,Notes")] Specimen specimen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specimen).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specimen);
        }

        // GET: BackOffice/Specimens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await db.SpecimenSet.FindAsync(id);
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
            Specimen specimen = await db.SpecimenSet.FindAsync(id);
            db.SpecimenSet.Remove(specimen);
            await db.SaveChangesAsync();
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

            var specimen = await db.SpecimenSet.FindAsync(id);

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
                        ScanDate = new DateTime(item.ScanYear, item.ScanMonth, item.ScanDay),
                        Process = item.Process,
                        OriginalFileName = item.OriginalFileName,
                        IsVisible = item.IsVisible,
                        FileName = newName + Path.GetExtension(file.FileName),
                        Encoding = file.ContentType
                    });

                    // Prepare to resize the pictures.
                    // We'll scale them proportionally to a maximum of 1024x768.
                    // We also can't dispose of the source image just yet.
                    ImageJob j = new ImageJob
                    {
                        Instructions = new Instructions
                        {
                            Width = 1024,
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
                    j.Instructions.Width = 500;
                    j.Instructions.Height = 300;
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
                db.DigitalPhotographSet.AddRange(photosToAdd);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest()) return PartialView(model);

            return View(model);
        }

        public async Task<ActionResult> ListPhotos(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var s = await db.SpecimenSet.FindAsync(id);

            if (s == null) { return HttpNotFound(); }

            return View(s.DigitalPhotographs.ToList());
        }

        [OutputCache(Location = OutputCacheLocation.Client, Duration = 60 * 60 * 24 * 7)]
        public async Task<ActionResult> GetPicture(int? id, string size = "Large")
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await db.DigitalPhotographSet.FindAsync(id);

            if (p == null /* || !p.IsVisible */) { return HttpNotFound(); }

            // Caching. Basically, we check if the browser has cached the picture (present if "If-Modified-Since" is in the headers).
            // If so, we check if the image was modified since then, and we'll return it.
            // From http://weblogs.asp.net/jeff/archive/2009/07/01/304-your-images-from-a-database.aspx

            // TODO?: There may be some implications with this. Need to check them better.

            if (!String.IsNullOrEmpty(Request.Headers["If-Modified-Since"]))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var lastMod = DateTime.ParseExact(Request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();

                // if (lastMod == p.LastModified.AddMilliseconds(-p.LastModified.Millisecond))
                if (lastMod.CompareTo(p.LastModified) > 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotModified);
                }
            }

            switch (size)
            {
                case "Large":
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", Path.GetFileNameWithoutExtension(p.FileName) + ".jpg"), p.Encoding);
                case "Thumb":
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Thumb", Path.GetFileNameWithoutExtension(p.FileName) + ".jpg"), p.Encoding);
                default:
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", Path.GetFileNameWithoutExtension(p.FileName) + ".jpg"), p.Encoding);
            }
        }

        public async Task<ActionResult> DeletePhoto(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await db.DigitalPhotographSet.FindAsync(id);

            if (p == null) { return new HttpStatusCodeResult(HttpStatusCode.NotFound); }

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeletePhoto")]
        public async Task<ActionResult> DeletePhotoConfirmed(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await db.DigitalPhotographSet.FindAsync(id);

            if (p == null) { return new HttpStatusCodeResult(HttpStatusCode.NotFound); }

            db.DigitalPhotographSet.Remove(p);

            // TODO: Remove all files from the disk.

            await db.SaveChangesAsync();

            return RedirectToAction("ListPhotos", new { id = p.SpecimenId });
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
