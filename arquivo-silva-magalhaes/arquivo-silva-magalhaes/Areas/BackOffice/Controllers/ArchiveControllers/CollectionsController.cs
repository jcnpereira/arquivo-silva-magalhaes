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
using ArquivoSilvaMagalhaes.Resources;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class CollectionsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/Collection/
        public async Task<ActionResult> Index(int? authorId)
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
            var model = new CollectionEditViewModel();

            var c = new Collection();
            c.LogoLocation = "dummy";
            c.Translations.Add(new CollectionTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });
            
            return View(await GenerateViewModel(c));
        }

        // POST: /BackOffice/Collection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            Collection collection, 
            HttpPostedFileBase Logo,
            int[] AuthorIds)
        {
            // Server-side check for an image.
            if (!Logo.ContentType.ToLower().StartsWith("image/"))
            {
                ModelState.AddModelError("Logo", ErrorStrings.Logo__MustBeImage);
                Logo.InputStream.Dispose();
            }
            
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
                    Source = Logo.InputStream,
                    Dest = Path.Combine(Server.MapPath("~/Public/Collections"), newName),
                    CreateParentDirectory = true
                };

                collection.LogoLocation = newName;

                var authors = db.Authors.Where(a => AuthorIds.Contains(a.Id));

                collection.Authors = authors.ToList();

                db.Collections.Add(collection);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(await GenerateViewModel(collection));
        }

        // GET: /BackOffice/Collection/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }

            var model = await GenerateViewModel(collection);

            return View(model);
        }

        // POST: /BackOffice/Collection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Collection collection, int[] AuthorIds)
        {
            if (ModelState.IsValid)
            {
                // "Force-load" the collection and the authors.
                db.Collections.Attach(collection);
                db.Entry(collection).Collection(c => c.Authors).Load();
                // The forced-loading was required so that the author list can be updated.
                var authors = db.Authors.Where(a => AuthorIds.Contains(a.Id)).ToList();
                collection.Authors = authors;

                db.Entry(collection).State = EntityState.Modified;

                foreach (var t in collection.Translations)
                {
                    db.Entry(t).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }


            return View(await GenerateViewModel(collection));
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

        private async Task<CollectionEditViewModel> GenerateViewModel(Collection c)
        {
            var vm = new CollectionEditViewModel();

            vm.Collection = c;

            var authorIds = c.Authors.Select(a => a.Id).ToList();

            vm.AvailableAuthors = db.Authors.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.LastName + ", " + a.FirstName,
                    Selected = authorIds.Contains(a.Id)
                })
                .ToList();

            return vm;
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
