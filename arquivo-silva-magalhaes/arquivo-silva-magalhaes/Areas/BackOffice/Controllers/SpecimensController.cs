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

            //var specimen = await db.SpecimenSet.FindAsync(id);

            //if (specimen == null) { return HttpNotFound(); }

            //return View(specimen);
            return View(new SpecimenPhotoUploadModel { SpecimenId = id.Value });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssociateDigitalPhoto(SpecimenPhotoUploadModel model)
        {
            // Debug.WriteLine("Got {0} files. Model is valid: {1}", files.Count(), ModelState.IsValid);

            Debug.WriteLine("Model is valid: {0}", ModelState.IsValid);

            // var specimen = await db.DigitalPhotographSet.FindAsync(model.SpecimenId);

            var photosToAdd = new List<DigitalPhotograph>();

            foreach (var item in model.Items.Where(i => i.IsToConsider))
            {
                var file = model.Photos.First(p => p.FileName == item.OriginalFileName);
                var newName = Guid.NewGuid().ToString();


                Debug.WriteLine(item.IsVisible);

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
                    ResetSourceStream = true,
                    DisposeSourceObject = false
                };

                ImageBuilder.Current.Build(j);

                j.Instructions.Width = 500;
                j.Instructions.Height = 300;
                j.DisposeSourceObject = false;
                j.Dest = Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Thumb", newName + ".jpg");

                ImageBuilder.Current.Build(j);

                file.SaveAs(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Original", newName + Path.GetExtension(file.FileName)));

                file.InputStream.Dispose();
            }

            db.DigitalPhotographSet.AddRange(photosToAdd);

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetPicture(int? id, string size = "Large")
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var p = await db.DigitalPhotographSet.FindAsync(id);

            if (p == null || !p.IsVisible) { return HttpNotFound(); }

            switch (size)
            {
                case "Large":
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", p.FileName + ".jpg"), p.Encoding);
                case "Thumb":
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Thumb", p.FileName + ".jpg"), p.Encoding);
                default:
                    return File(Path.Combine(Server.MapPath("~/App_Data/Uploads/Specimens"), "Large", p.FileName + ".jpg"), p.Encoding);
            }
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
