﻿using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Web.I18n;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using ArquivoSilvaMagalhaes.Common;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class FormatsController : ArchiveControllerBase
    {
        private IRepository<Format> db;

        public FormatsController()
            : this(new GenericDbRepository<Format>()) { }

        public FormatsController(IRepository<Format> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Formats
        public async Task<ActionResult> Index(int pageNumber = 1, string query = "")
        {
            var model = await db.Entities
                .Where(f => query == "" || f.FormatDescription.Contains(query))
                .OrderBy(f => f.Id)
                .ToPagedListAsync(pageNumber, 10);

            ViewBag.Query = query;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        // GET: BackOffice/Formats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.GetByIdAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // GET: BackOffice/Formats/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: BackOffice/Formats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude = "Id")]Format format)
        {
            if (DoesFormatExist(format))
            {
                ModelState.AddModelError("FormatDescription", FormatStrings.Validation_FormatAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                db.Add(format);

                await db.SaveChangesAsync();



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
            Format format = await db.GetByIdAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // POST: BackOffice/Formats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FormatDescription")] Format format)
        {
            if (DoesFormatExist(format))
            {
                ModelState.AddModelError("FormatDescription", FormatStrings.Validation_FormatAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                db.Update(format);
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
            Format format = await db.GetByIdAsync(id);
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
            Format format = await db.GetByIdAsync(id);
            db.Remove(format);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DoesFormatExist(Format format)
        {
            return db.Entities
                .Any(f => f.Id != format.Id && f.FormatDescription == format.FormatDescription);
        }

        public ActionResult AuxAdd()
        {
            var model = new Format();

            return PartialView("_FormatFields", model);
        }

        [HttpPost]
        public async Task<ActionResult> AuxAdd(Format t)
        {
            var cl = db.Entities
                .FirstOrDefault(c => t.FormatDescription == c.FormatDescription);

            if (cl == null)
            {
                cl = t;
                db.Add(t);
                await db.SaveChangesAsync();
            }

            return Json((await db.Entities
                .OrderBy(ct => ct.Id)
                .ToListAsync())
                .Select(ct => new
                {
                    value = ct.Id.ToString(),
                    text = ct.FormatDescription,
                    selected = ct.Id == cl.Id
                })
                .ToList());
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