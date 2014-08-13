using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class BannerController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /Banner/
        public async Task<ActionResult> Index()
        {
            return View(await db.Banners.ToListAsync());
        }

        // GET: /Banner/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = await db.Banners.FindAsync(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: /Banner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Banner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Image,UriPath,PublicationDate,RemovalDate,IsVisible,LanguageCode,Title")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Banners.Add(banner);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: /Banner/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = await db.Banners.FindAsync(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: /Banner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Image,UriPath,PublicationDate,RemovalDate,IsVisible,LanguageCode,Title")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banner).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: /Banner/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = await db.Banners.FindAsync(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: /Banner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Banner banner = await db.Banners.FindAsync(id);
            db.Banners.Remove(banner);
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
