using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ReferencedLinkController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/ReferencedLink/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => 
                db.ReferencedLinks
                  .OrderBy(l => l.Id)
                  .ToPagedList(pageNumber, 10)));
        }

        // GET: /BackOffice/ReferencedLink/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }
            return View(referencedlink);
        }

        // GET: /BackOffice/ReferencedLink/Create
        public ActionResult Create()
        {
            return View(new ReferencedLink());
        }

        // POST: /BackOffice/ReferencedLink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReferencedLink referencedlink)
        {
            if (ModelState.IsValid)
            {
                db.ReferencedLinks.Add(referencedlink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(referencedlink);
        }

        // GET: /BackOffice/ReferencedLink/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }
            return View(referencedlink);
        }

        // POST: /BackOffice/ReferencedLink/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ReferencedLink referencedlink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referencedlink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(referencedlink);
        }

        // GET: /BackOffice/ReferencedLink/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }
            return View(referencedlink);
        }

        // POST: /BackOffice/ReferencedLink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
            db.ReferencedLinks.Remove(referencedlink);
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
