using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Resources;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class FormatsController : ArchiveControllerBase
    {
        private IRepository<Format> db;

        public FormatsController()
            : this(new GenericDbRepository<Format>()) { }

        public FormatsController(IRepository<Format> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Formats
        public ActionResult Index(int pageNumber = 1)
        {
            return View(db.Entities.OrderBy(f => f.Id).ToPagedList(pageNumber, 10));
        }

        // GET: BackOffice/Formats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.GetByIdAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // GET: BackOffice/Formats/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_FormatFields");
            }

            return View();
        }

        // POST: BackOffice/Formats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "Id")]Format format)
        {
            if (DoesFormatExist(format))
            {
                ModelState.AddModelError("FormatDescription", FormatStrings.Validation_FormatAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                db.Add(format);

                await db.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                {
                    var result = new List<object>();
                    result.Add(new
                    {
                        value = "",
                        text = UiPrompts.ChooseOne
                    });

                    result.AddRange(
                        db.Entities
                          .OrderBy(f => f.Id)
                          .Select(f => new
                          {
                              value = f.Id.ToString(),
                              text = f.FormatDescription
                          }));

                    return Json(result);
                }

                return RedirectToAction("Index");
            }

            return View(format);
        }

        // GET: BackOffice/Formats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.GetByIdAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // POST: BackOffice/Formats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FormatDescription")] Format format)
        {
            if (DoesFormatExist(format))
            {
                ModelState.AddModelError("FormatDescription", FormatStrings.Validation_FormatAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                db.Update(format);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(format);
        }

        // GET: BackOffice/Formats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.GetByIdAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // POST: BackOffice/Formats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Format format = await db.GetByIdAsync(id);
            db.Remove(format);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DoesFormatExist(Format format)
        {
            return db.Entities
                .Any(f => f.Id != format.Id && f.FormatDescription == format.FormatDescription);
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