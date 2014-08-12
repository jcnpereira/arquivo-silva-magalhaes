using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ArchivesController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /Archives/
        public async Task<ActionResult> Index()
        {
            return View(await db.Archives.ToListAsync());
        }

        // GET: /Archives/Details/5
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

        // GET: /Archives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Archives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Title,LanguageCode,ArchiveMission,ArchiveHistory,Name,Email,Address,ContactDetails,Service")] Archive archive)
        {
            if (ModelState.IsValid)
            {
                db.Archives.Add(archive);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(archive);
        }

        // GET: /Archives/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: /Archives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Title,LanguageCode,ArchiveMission,ArchiveHistory,Name,Email,Address,ContactDetails,Service")] Archive archive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archive).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(archive);
        }

        // GET: /Archives/Delete/5
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

        // POST: /Archives/Delete/5
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
