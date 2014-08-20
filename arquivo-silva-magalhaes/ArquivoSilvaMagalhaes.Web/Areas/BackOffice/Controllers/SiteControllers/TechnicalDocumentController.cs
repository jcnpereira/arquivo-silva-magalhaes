using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.SiteViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class TechnicalDocumentController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/TechnicalDocument/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() =>
                db.TechnicalDocuments
                  .OrderBy(td => td.Id)
                  .ToPagedList(pageNumber = 1, 10)));
        }

        // GET: /BackOffice/TechnicalDocument/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            if (technicaldocument == null)
            {
                return HttpNotFound();
            }
            return View(technicaldocument);
        }

        // GET: /BackOffice/TechnicalDocument/Create
        public ActionResult Create()
        {
            return View(GenerateViewModel(new TechnicalDocument()));
        }

        // POST: /BackOffice/TechnicalDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TechnicalDocument technicalDocument, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                technicalDocument.UploadDate = DateTime.Now;

                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadedFile.FileName);

                var dir = Server.MapPath("~/Public/TechnicalDocuments/");

                Directory.CreateDirectory(dir);

                uploadedFile.SaveAs(dir + fileName);

                technicalDocument.FileName = fileName;
                technicalDocument.Format = uploadedFile.ContentType;
                technicalDocument.FileSize = uploadedFile.ContentLength;
                

                db.TechnicalDocuments.Add(technicalDocument);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(technicalDocument);
        }

        // GET: /BackOffice/TechnicalDocument/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            if (technicaldocument == null)
            {
                return HttpNotFound();
            }
            return View(GenerateViewModel(technicaldocument));
        }

        // POST: /BackOffice/TechnicalDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            TechnicalDocument technicaldocument,
            HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                var doc = await db.TechnicalDocuments.FindAsync(technicaldocument.Id);
         
                if (uploadedFile != null)
                {
                    var fileName = doc.FileName;
                    uploadedFile.SaveAs(Server.MapPath("~/Public/TechnicalDocuments/") + fileName);
                    doc.Format = uploadedFile.ContentType;
                    doc.FileSize = uploadedFile.ContentLength;
                }
                doc.Title = technicaldocument.Title;
                doc.LastModificationDate = DateTime.Now;
                db.Entry(doc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(GenerateViewModel(technicaldocument));
        }

        // GET: /BackOffice/TechnicalDocument/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            if (technicaldocument == null)
            {
                return HttpNotFound();
            }
            return View(technicaldocument);
        }

        // POST: /BackOffice/TechnicalDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            db.TechnicalDocuments.Remove(technicaldocument);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private TechnicalDocumentEditViewModel GenerateViewModel(TechnicalDocument document)
        {
            var model = new TechnicalDocumentEditViewModel
            {
                TechnicalDocument = document
            };
            return model;
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
