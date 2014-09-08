using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ImageResizer;
using PagedList;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class PartnershipsController : SiteControllerBase
    {
        private IRepository<Partnership> db;

        public PartnershipsController()
            : this(new GenericDbRepository<Partnership>()) { }

        public PartnershipsController(IRepository<Partnership> db)
        {
            this.db = db;
        }

        // GET: /BackOffice/Parthnership/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities.ToListAsync()).ToPagedList(pageNumber, 10));
        }

        // GET: /BackOffice/Parthnership/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = await db.GetByIdAsync(id);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(partnership);
        }

        // GET: /BackOffice/Parthnership/Create
        public ActionResult Create()
        {
            return View(new PartnershipEditViewModel { Partnership = new Partnership() });
        }

        // POST: /BackOffice/Parthnership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PartnershipEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Upload != null)
                {
                    var newName = Guid.NewGuid().ToString() + ".jpg";

                    FileUploadHelper.SaveImage(model.Upload.InputStream,
                        400, 400,
                        Server.MapPath("~/Public/Partnerships/") + newName,
                        FitMode.Crop);

                    model.Partnership.LogoFileName = newName;
                }


                db.Add(model.Partnership);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /BackOffice/Parthnership/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = await db.GetByIdAsync(id);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(new PartnershipEditViewModel
                {
                    Partnership = partnership
                });
        }

        // POST: /BackOffice/Parthnership/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PartnershipEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Update(model.Partnership);

                // Update the logo if a new one is supplied. Don't allow property value changes if
                // the logo doesn't exist.
                if (model.Upload != null)
                {
                    var logo = db.GetValueFromDb(model.Partnership, p => p.LogoFileName);

                    if (logo == null)
                    {
                        model.Partnership.LogoFileName =
                            Guid.NewGuid().ToString() + "_" + model.Upload.FileName;

                        logo = model.Partnership.LogoFileName;
                    }

                    model.Partnership.LogoFileName = logo;

                    FileUploadHelper.SaveImage(model.Upload.InputStream,
                        400, 400,
                        Server.MapPath("~/Public/Partnerships/") + logo,
                        FitMode.Crop);
                }
                else
                {
                    db.ExcludeFromUpdate(model.Partnership, p => new { p.LogoFileName });
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /BackOffice/Parthnership/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = await db.GetByIdAsync(id);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(partnership);
        }

        // POST: /BackOffice/Parthnership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Partnership partnership = await db.GetByIdAsync(id);
            db.Remove(partnership);

            if (partnership.LogoFileName != null)
            {
                var fullPath = Server.MapPath("~/Public/Partnerships/" + partnership.LogoFileName);

                // Remove file from the disk.
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
