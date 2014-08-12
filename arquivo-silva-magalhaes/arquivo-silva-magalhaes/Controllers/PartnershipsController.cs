using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class PartnershipsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /Partnerships/
        public async Task<ActionResult> Index()
        {
            return View(await db.Partnerships.ToListAsync());
        }

        // GET: /Partnerships/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = await db.Partnerships.FindAsync(id);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(partnership);
        }

        // GET: /Partnerships/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Partnerships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,Logo,SiteLink,EmailAddress,Contact,PartnershipType")] Partnership partnership)
        {
            if (ModelState.IsValid)
            {
                db.Partnerships.Add(partnership);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(partnership);
        }

        // GET: /Partnerships/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = await db.Partnerships.FindAsync(id);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(partnership);
        }

        // POST: /Partnerships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Logo,SiteLink,EmailAddress,Contact,PartnershipType")] Partnership partnership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partnership).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(partnership);
        }

        // GET: /Partnerships/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = await db.Partnerships.FindAsync(id);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(partnership);
        }

        // POST: /Partnerships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Partnership partnership = await db.Partnerships.FindAsync(id);
            db.Partnerships.Remove(partnership);
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
