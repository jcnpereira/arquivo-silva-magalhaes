using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Common;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class DocumentAttachmentController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/DocumentAttachment/
        public async Task<ActionResult> Index()
        {
            return View(await db.Attachments.ToListAsync());
        }

        // GET: /BackOffice/DocumentAttachment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment documentattachment = await db.Attachments.FindAsync(id);
            if (documentattachment == null)
            {
                return HttpNotFound();
            }
            return View(documentattachment);
        }

        // GET: /BackOffice/DocumentAttachment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BackOffice/DocumentAttachment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Attachment model)
        {
            if (ModelState.IsValid)
            {
                db.Attachments.Add(model);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }




        // GET: /BackOffice/DocumentAttachment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment documentattachment = await db.Attachments.FindAsync(id);
            if (documentattachment == null)
            {
                return HttpNotFound();
            }
            return View(documentattachment);
        }

        // POST: /BackOffice/DocumentAttachment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,MimeFormat,UriPath,Size")] Attachment documentattachment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentattachment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(documentattachment);
        }

        // GET: /BackOffice/DocumentAttachment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment documentattachment = await db.Attachments.FindAsync(id);
            if (documentattachment == null)
            {
                return HttpNotFound();
            }
            return View(documentattachment);
        }

        // POST: /BackOffice/DocumentAttachment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Attachment documentattachment = await db.Attachments.FindAsync(id);
            db.Attachments.Remove(documentattachment);
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
