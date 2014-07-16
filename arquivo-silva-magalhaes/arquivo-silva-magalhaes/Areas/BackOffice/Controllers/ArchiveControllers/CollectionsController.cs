using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Utilitites;
using ImageResizer;
using System.IO;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class CollectionsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/Collection/
        public async Task<ActionResult> Index()
        {
            return View(await db.Collections.ToListAsync());
        }

        // GET: /BackOffice/Collection/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: /BackOffice/Collection/Create
        public async Task<ActionResult> Create()
        {
            return View(new CollectionEditViewModel
                {
                    AvailableAuthors = await db.Authors.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.FirstName + " " + a.LastName
                    }).ToListAsync(),

                    Translations = new List<CollectionTranslationEditViewModel>
                    {
                        new CollectionTranslationEditViewModel { LanguageCode = LanguageDefinitions.DefaultLanguage }
                    }
                });
        }

        // POST: /BackOffice/Collection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "LogoLocation")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                var newName = Guid.NewGuid().ToString() + ".jpg";

                // Prepare to resize the pictures.
                // We'll scale them proportionally to a maximum of 1024x768.
                ImageJob j = new ImageJob
                {
                    Instructions = new Instructions
                    {
                        Width = 1024,
                        Height = 768,
                        Mode = FitMode.Max,
                        Encoder = "freeimage",
                        OutputFormat = OutputFormat.Jpeg
                    },
                    Source = Request.Files["Logo"].InputStream,
                    Dest = Path.Combine(Server.MapPath("~/Public/Collections"), newName),
                    CreateParentDirectory = true
                };

                collection.LogoLocation = newName;

                db.Collections.Add(collection);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(collection);
        }

        // GET: /BackOffice/Collection/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }

            var t = collection.Translations.First(text => text.LanguageCode == LanguageDefinitions.DefaultLanguage);

            var model = new CollectionEditViewModel
            {
                Id = collection.Id,
                
                Name = collection.Name,
                AttachmentsDescriptions = collection.AttachmentsDescriptions,
                CatalogCode = collection.CatalogCode,
                EndProductionDate = collection.EndProductionDate,
                HasAttachments = collection.HasAttachments,
                InitialProductionDate = collection.InitialProductionDate,
                IsVisible = collection.IsVisible,
                LogoLocation = collection.LogoLocation,
                Notes = collection.Notes,
                OrganizationSystem = collection.OrganizationSystem,
                Type = collection.Type,

                Translations = new List<CollectionTranslationEditViewModel>{
                   new CollectionTranslationEditViewModel{
                       CollectionId=collection.Id,
                       LanguageCode=t.LanguageCode,
                       Title=t.Title,
                       Description=t.Description,
                       Provenience=t.Provenience,
                       AdministrativeAndBiographicStory=t.AdministrativeAndBiographicStory,
                       FieldAndContents=t.FieldAndContents,
                       Dimension=t.Dimension,
                       CopyrightInfo=t.CopyrightInfo,

                   }
                }
            };
            return View(model);
        }

        // POST: /BackOffice/Collection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Type,InitialProductionDate,EndProductionDate,LogoLocation,HasAttachments,OrganizationSystem,Notes,IsVisible,CatalogCode")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                foreach (var t in collection.Translations)
                {
                    db.Entry(t).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(collection);
        }

        // GET: /BackOffice/Collection/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.Collections.FindAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: /BackOffice/Collection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Collection collection = await db.Collections.FindAsync(id);
            db.Collections.Remove(collection);
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
