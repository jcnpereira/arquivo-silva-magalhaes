using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Utilitites;
using System.Globalization;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using System.IO;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
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
        public async Task<ActionResult> Create( DocumentAttachmentViewModels model)
        {
            if (ModelState.IsValid)
            {
                

                var documentattachment = new Attachment
                {
                    Id=model.Id,
                    UriPath = model.UriPath,
                    MimeFormat = model.MimeFormat,
                    Size = model.Size,
                    Title=model.Title,
                    LanguageCode=model.LanguageCode,
                    Description=model.Description
                };

                //documentattachment.TextUsingAttachment.Add(
                //    new AttachmentTranslation
                //    {
                //        Title = model.Title,
                //        Description = model.Description,
                //        LanguageCode = LanguageDefinitions.DefaultLanguage,
                      
                //    });
                db.Attachments.Add(documentattachment);
                await db.SaveChangesAsync();

                //if (AreLanguagesMissing( documentattachment))
                //{
                //    // There are languages which may be added.
                //    // Ask the used if he/she wants to any.
                //    ViewBag.Id = documentattachment.Id;
                //    return View("_AddLanguagePrompt");
                //}


                return RedirectToAction("Index");
            }

            return View(model);
        }

        
        private static bool AreLanguagesMissing(Attachment documentattachment)
        {
            // Check if we need to add more languages.
            var langCodes = documentattachment.TextUsingAttachment
                                  .Select(t => t.LanguageCode)
                                  .ToList();

            return LanguageDefinitions.Languages.Where(l => !langCodes.Contains(l)).Count() > 0;
        }

        // GET: BackOffice/Authors/AddLanguage
        public async Task<ActionResult> AddLanguage(int? Id)
        {
            // There needs to be an author.
            if (Id != null)
            {
                var documentattachment = await db.Attachments.FindAsync(Id);

                if (documentattachment == null)
                {
                    return HttpNotFound();
                }

                var langCodes = documentattachment.TextUsingAttachment
                                      .Select(t => t.LanguageCode)
                                      .ToList();

                // First, we'll check which languages we already have in the DB,
                // we'll remove any which already exist.
                var notDoneLanguages = LanguageDefinitions.Languages
                                                          .Where(l => !langCodes.Contains(l));

                // This should stop naughty attempts at adding a language
                // to an entity which already has all languages done.
                if (notDoneLanguages.Count() == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var model = new DocumentAttachmentViewModels
                {
                    AvailableLanguages = notDoneLanguages
                                            .Select(l => new SelectListItem
                                            {
                                                // Get the localized language name.
                                                Text = CultureInfo.GetCultureInfo(l).NativeName,
                                                // Text = LanguageDefinitions.GetLanguageNameForCurrentLanguage(l),
                                                Value = l
                                            })
                                            .ToList()
                };

                return View(model);
            }
            else
            {
                // If there's no author, we can't add a language to it.
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddLanguage(DocumentI18nPartialModels model)
        {
            if (ModelState.IsValid)
            {
                var documentattachmenttext = db.Attachments.Find(model.Id);

                var documenttext = new AttachmentTranslation
                {
                    Title = model.Title,
                    LanguageCode = model.LanguageCode,
                    Description = model.Description
                };

                db.AttachmentTranslations.Add(documenttext);
                await db.SaveChangesAsync();

                if (AreLanguagesMissing(documentattachmenttext))
                {
                    ViewBag.Id = documentattachmenttext.Id;
                    return View("_AddLanguagePrompt");
                }

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
