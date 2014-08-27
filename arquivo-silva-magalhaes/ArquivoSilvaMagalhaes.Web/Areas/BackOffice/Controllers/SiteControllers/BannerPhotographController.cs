using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels;
using ArquivoSilvaMagalhaes.Common;
using ImageResizer;
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
    public class BannerPhotographController : SiteControllerBase
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/BannerPhotograph/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => 
                db.BannerTranslations
                  .Include(bt => bt.BannerPhotograph)
                  .Where(bt => bt.LanguageCode == LanguageDefinitions.DefaultLanguage)
                  .OrderBy(bt => bt.BannerPhotographId)
                  .ToPagedList(pageNumber, 10)));
        }

        // GET: /BackOffice/BannerPhotograph/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner bannerphotograph = await db.Banners.FindAsync(id);
            if (bannerphotograph == null)
            {
                return HttpNotFound();
            }
            return View(bannerphotograph);
        }

        // GET: /BackOffice/BannerPhotograph/Create
        public ActionResult Create()
        {
            var banner = new Banner();
            banner.Translations.Add(new BannerTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });
            return View(GenerateViewModel(banner));
        }

        // POST: /BackOffice/BannerPhotograph/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Banner banner, HttpPostedFileBase image)
        {
            if (image == null)
            {
                ModelState.AddModelError("Image", "");
            }

            if (ModelState.IsValid)
            {
                var newName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);

                ImageJob j = new ImageJob
                {
                    Instructions = new Instructions
                    {
                        Width = 1024,
                        Height = 500,
                        Mode = FitMode.Crop,
                        Encoder = "freeimage",
                        OutputFormat = OutputFormat.Jpeg
                    },
                    Source = image.InputStream,
                    Dest = Path.Combine(Server.MapPath("~/Public/Banners"), newName),
                    CreateParentDirectory = true
                };

                ImageBuilder.Current.Build(j);

                banner.UriPath = newName;

                db.Banners.Add(banner);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(banner));
        }

        // GET: /BackOffice/BannerPhotograph/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner bannerphotograph = await db.Banners.FindAsync(id);
            if (bannerphotograph == null)
            {
                return HttpNotFound();
            }
            return View(GenerateViewModel(bannerphotograph));
        }

        // POST: /BackOffice/BannerPhotograph/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banner).State = EntityState.Modified;
                db.Entry(banner).Property(b => b.UriPath).IsModified = false;

                foreach (var item in banner.Translations)
                {
                    db.Entry(item).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(GenerateViewModel(banner));
        }

        // GET: /BackOffice/BannerPhotograph/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner bannerphotograph = await db.Banners.FindAsync(id);
            if (bannerphotograph == null)
            {
                return HttpNotFound();
            }
            return View(bannerphotograph);
        }

        // POST: /BackOffice/BannerPhotograph/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Banner bannerphotograph = await db.Banners.FindAsync(id);
            db.Banners.Remove(bannerphotograph);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private BannerPhotographEditViewModel GenerateViewModel(Banner banner)
        {
            var model = new BannerPhotographEditViewModel
            {
                Banner = banner
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
