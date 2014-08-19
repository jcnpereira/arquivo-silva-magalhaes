using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using PagedList;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class PartnershipsController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/Parthnership/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => db.Partnerships
                .OrderBy(p => p.Id)
                .ToPagedList(pageNumber, 10)));
        }

        // GET: /BackOffice/Parthnership/Details/5
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

        // GET: /BackOffice/Parthnership/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BackOffice/Parthnership/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Partnership partnership)
        {
            if (ModelState.IsValid)
            {
                db.Partnerships.Add(partnership);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(partnership);
        }

        // GET: /BackOffice/Parthnership/Edit/5
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

        // POST: /BackOffice/Parthnership/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Logo,SiteLink,EmailAddress,Contact,PartnershipType")] Partnership partnership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partnership).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(partnership);
        }

        // GET: /BackOffice/Parthnership/Delete/5
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

        // POST: /BackOffice/Parthnership/Delete/5
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
