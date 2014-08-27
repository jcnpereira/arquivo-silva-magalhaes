using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class SpotLightVideoController : SiteControllerBase
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/SpotLightVideo/
        public async Task<ActionResult> Index()
        {
            return View(await db.SpotlightVideos.ToListAsync());
        }

        // GET: /BackOffice/SpotLightVideo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpotlightVideo spotlightvideo = await db.SpotlightVideos.FindAsync(id);
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
        public async Task<ActionResult> Create( SpotlightVideo spotlightVideo)
        {
            if (ModelState.IsValid)
            {
                db.SpotlightVideos.Add(spotlightVideo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(spotlightVideo);
        }

        // GET: /BackOffice/SpotLightVideo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpotlightVideo spotlightvideo = await db.SpotlightVideos.FindAsync(id);
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
            SpotlightVideo spotlightvideo = await db.SpotlightVideos.FindAsync(id);
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
            SpotlightVideo spotlightvideo = await db.SpotlightVideos.FindAsync(id);
            db.SpotlightVideos.Remove(spotlightvideo);
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
