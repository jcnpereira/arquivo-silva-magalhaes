﻿using System;
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

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class SpotLightVideoController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/SpotLightVideo/
        public async Task<ActionResult> Index()
        {
            return View(await db.SpotlightVideoSet.ToListAsync());
        }

        // GET: /BackOffice/SpotLightVideo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpotlightVideo spotlightvideo = await db.SpotlightVideoSet.FindAsync(id);
            if (spotlightvideo == null)
            {
                return HttpNotFound();
            }
            return View(spotlightvideo);
        }

        // GET: /BackOffice/SpotLightVideo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BackOffice/SpotLightVideo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,UriPath,PublicationDate,RemotionDate,IsPermanent")] SpotlightVideo spotlightvideo)
        {
            if (ModelState.IsValid)
            {
                db.SpotlightVideoSet.Add(spotlightvideo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(spotlightvideo);
        }

        // GET: /BackOffice/SpotLightVideo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpotlightVideo spotlightvideo = await db.SpotlightVideoSet.FindAsync(id);
            if (spotlightvideo == null)
            {
                return HttpNotFound();
            }
            return View(spotlightvideo);
        }

        // POST: /BackOffice/SpotLightVideo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,UriPath,PublicationDate,RemotionDate,IsPermanent")] SpotlightVideo spotlightvideo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spotlightvideo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(spotlightvideo);
        }

        // GET: /BackOffice/SpotLightVideo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpotlightVideo spotlightvideo = await db.SpotlightVideoSet.FindAsync(id);
            if (spotlightvideo == null)
            {
                return HttpNotFound();
            }
            return View(spotlightvideo);
        }

        // POST: /BackOffice/SpotLightVideo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SpotlightVideo spotlightvideo = await db.SpotlightVideoSet.FindAsync(id);
            db.SpotlightVideoSet.Remove(spotlightvideo);
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
