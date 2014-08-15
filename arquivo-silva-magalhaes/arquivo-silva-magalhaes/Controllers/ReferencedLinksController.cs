using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ReferencedLinksController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /ReferencedLinks/
        public async Task<ActionResult> Index()
        {
            return View(await db.ReferencedLinks.ToListAsync());
        }

        // GET: /ReferencedLinks/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
        //    if (referencedlink == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(referencedlink);
        //}

        //// GET: /ReferencedLinks/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /ReferencedLinks/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include="Id,Title,Link,Description,DateOfCreation,LastModifiedDate,IsUsefulLink")] ReferencedLink referencedlink)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ReferencedLinks.Add(referencedlink);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(referencedlink);
        //}

        //// GET: /ReferencedLinks/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
        //    if (referencedlink == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(referencedlink);
        //}

        //// POST: /ReferencedLinks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include="Id,Title,Link,Description,DateOfCreation,LastModifiedDate,IsUsefulLink")] ReferencedLink referencedlink)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(referencedlink).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(referencedlink);
        //}

        //// GET: /ReferencedLinks/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
        //    if (referencedlink == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(referencedlink);
        //}

        //// POST: /ReferencedLinks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    ReferencedLink referencedlink = await db.ReferencedLinks.FindAsync(id);
        //    db.ReferencedLinks.Remove(referencedlink);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
