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
using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class PortalController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /Portal/
        public async Task<ActionResult> Index()
        {
            return View(await db.Archives.ToListAsync());
        }

        // GET: /Portal/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = await db.Archives.FindAsync(id);
            if (archive == null)
            {
                return HttpNotFound();
            }
            return View(archive);
        }

        // GET: /Portal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Portal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PortalViewModels model)
        {
            if (ModelState.IsValid)
            {
                var archive = new Archive
                {
                    Title = model.Title,
                    LanguageCode = model.LanguageCode,
                    ArchiveMission = model.ArchiveMission,
                    ArchiveHistory = model.ArchiveHistory,
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    ContactDetails = model.ContactDetails,
                    Service = model.Service
                };

                archive.ArchiveTranslations.Add(
               new ArchiveTranslations
               {
                   LanguageCode = model.LanguageCode,
                   ArchiveHistory = model.ArchiveHistory,
                   ArchiveMission = model.ArchiveMission
               });


                archive.Contact.Add(
                new Contact
                {
                    Name = model.Name,
                    Address = model.Address,
                    Email = model.Email,
                    ContactDetails = model.ContactDetails,
                    Service = model.Service
                });
                db.Archives.Add(archive);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: /Portal/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = await db.Archives.FindAsync(id);
            if (archive == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var model = new PortalViewModels
            {
                Id = archive.Id,
                Title = archive.Title,

                Address = archive.Address,
                ArchiveHistory = archive.ArchiveHistory,
                ArchiveMission = archive.ArchiveMission,
                ContactDetails = archive.ContactDetails,
                Email = archive.Email,
                LanguageCode = archive.LanguageCode,
                Name = archive.Name,
                Service = archive.Service
            };
            return View(archive);
        }

        // POST: /Portal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PortalViewModels model)
        {
            if (ModelState.IsValid)
            {
                
                var portal = db.Archives.Find(model.Id);
                portal.Title = model.Title;
                portal.Address = model.Address;
                portal.ArchiveHistory = model.ArchiveHistory;
                portal.ArchiveMission = model.ArchiveMission;
                portal.ContactDetails = model.ContactDetails;
                portal.Email = model.Email;
                portal.LanguageCode = model.LanguageCode;
                portal.Name = model.Name;
                portal.Service = model.Service;

              /*  var text = db.ArchiveTranslations.Find(model.Id);
                text.ArchiveMission = model.ArchiveMission;
                text.ArchiveHistory = model.ArchiveHistory;
                text.LanguageCode = model.LanguageCode;

                var contact = db.ArchiveContacts.Find(model.Id);
                contact.Name = model.Name;
                contact.Email = model.Email;
                contact.ContactDetails = model.ContactDetails;
                contact.Address = model.Address;
                contact.Service = model.Service;

                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index"); */
            }

         //   if (ModelState.IsValid)
        //    {
      //         db.Entry(model).State = EntityState.Modified;

                await db.SaveChangesAsync(); 
               return RedirectToAction("Index");
     //       }
        }

        // GET: /Portal/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = await db.Archives.FindAsync(id);
            if (archive == null)
            {
                return HttpNotFound();
            }
            return View(archive);
        }

        // POST: /Portal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Archive archive = await db.Archives.FindAsync(id);
            db.Archives.Remove(archive);
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
