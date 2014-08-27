using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class FormatsController : ArchiveControllerBase
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Formats
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(
                await Task.Run(() => db.Formats.OrderBy(f => f.Id).ToPagedList(pageNumber, 10))
            );
        }

        // GET: BackOffice/Formats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.Formats.FindAsync(id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "Id")]Format format)
        {
            if (ModelState.IsValid)
            {
                db.Formats.Add(format);
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
                        db.Formats
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
            Format format = await db.Formats.FindAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // POST: BackOffice/Formats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FormatDescription")] Format format)
        {
            if (ModelState.IsValid)
            {
                db.Entry(format).State = EntityState.Modified;
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
            Format format = await db.Formats.FindAsync(id);
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
            Format format = await db.Formats.FindAsync(id);
            db.Formats.Remove(format);
            await db.SaveChangesAsync();
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
