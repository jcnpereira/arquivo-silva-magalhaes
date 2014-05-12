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

            foreach (var item in model.Items)
            {
                var fileName = Path.GetFileName(item.Photo.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads/Specimens"), model.SpecimenId.ToString());

                Directory.CreateDirectory(path);

                photosToAdd.Add(new DigitalPhotograph
                    {
                        SpecimenId = model.SpecimenId,
                        CopyrightInfo = item.CopyrightInfo,
                        IsVisible = item.IsVisible.ToString(),
                        StoreLocation = Path.Combine(path, fileName),
                        Process = item.Process,
                        ScanDate = new DateTime(item.ScanYear, item.ScanMonth, item.ScanDay)
                    });

                Debug.WriteLine("Path is: {0}", path);


                item.Photo.SaveAs(Path.Combine(path, fileName));
            }

            return RedirectToAction("Index");
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
