using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class AttachmentsController : SiteControllerBase
    {
        private IRepository<Attachment> db;

        public AttachmentsController()
            : this(new GenericDbRepository<Attachment>()) { }

        public AttachmentsController(IRepository<Attachment> db)
        {
            this.db = db;
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<string> fileNames)
        {
            var model = new AttachmentEditViewModel
                {
                    Attachments = fileNames.Select(n => new Attachment
                                  {
                                      Title = n,
                                      FileName = n
                                  }).ToList()
                };

            return PartialView(model);
        }

        // POST: /BackOffice/DocumentAttachment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Attachment documentattachment = await db.GetByIdAsync(id);

            db.Remove(documentattachment);
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
