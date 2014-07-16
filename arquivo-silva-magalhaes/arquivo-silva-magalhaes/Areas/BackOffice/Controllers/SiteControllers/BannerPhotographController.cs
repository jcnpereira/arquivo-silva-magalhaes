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
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using System.IO;
using ArquivoSilvaMagalhaes.Models.SiteViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class BannerPhotographController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/BannerPhotograph/
        public async Task<ActionResult> Index()
        {
            return View(await db.Banners.ToListAsync());
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
            var model = new PhotographViewModel
            {
                I18nTexts = new List<PhotographI18nViewModel>
                {
                    new PhotographI18nViewModel { LanguageCode = LanguageDefinitions.DefaultLanguage }
                }
            };
            return View(model);
        }

        // POST: /BackOffice/BannerPhotograph/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PhotographViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                // TODO: Talvez redimensionar?
                //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

                var banner = new Banner
                {
                   // UriPath = Path.Combine("Public/BannerPhotographs", fileName),
                    UriPath=model.UriPath,
                    PublicationDate = model.PublicationDate,
                    RemovalDate = model.RemovalDate,
                    IsVisible = model.IsVisible
                };

                //banner.BannerTexts.Add(
                //    new BannerTranslation
                //    {
                //        BannerPhotographId = banner.Id,
                //        LanguageCode = LanguageDefinitions.DefaultLanguage,
                //        Title = model.I18nTexts[0].Title
                //    });

                db.Banners.Add(banner);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
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
            return View(bannerphotograph);
        }

        // POST: /BackOffice/BannerPhotograph/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ImageData,UriPath,PublicationDate,RemovalDate,IsVisible")] Banner bannerphotograph)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bannerphotograph).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bannerphotograph);
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
