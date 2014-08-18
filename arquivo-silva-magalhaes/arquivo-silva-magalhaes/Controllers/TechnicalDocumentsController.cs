using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class TechnicalDocumentsController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /TechnicalDocuments/
        public async Task<ActionResult> Index()
        {
            return View(await db.TechnicalDocuments.ToListAsync());
        }

        // GET: /TechnicalDocuments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
            if (technicaldocument == null)
            {
                return HttpNotFound();
            }
            return View(technicaldocument);
        }

        // GET: /TechnicalDocuments/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /TechnicalDocuments/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include="Id,Title,FileName,UploadDate,LastModificationDate,Format,DocumentType,Language,FileSize")] TechnicalDocument technicaldocument)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TechnicalDocuments.Add(technicaldocument);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(technicaldocument);
        //}

        //// GET: /TechnicalDocuments/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
        //    if (technicaldocument == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(technicaldocument);
        //}

        //// POST: /TechnicalDocuments/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include="Id,Title,FileName,UploadDate,LastModificationDate,Format,DocumentType,Language,FileSize")] TechnicalDocument technicaldocument)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(technicaldocument).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(technicaldocument);
        //}

        //// GET: /TechnicalDocuments/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
        //    if (technicaldocument == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(technicaldocument);
        //}

        //// POST: /TechnicalDocuments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    TechnicalDocument technicaldocument = await db.TechnicalDocuments.FindAsync(id);
        //    db.TechnicalDocuments.Remove(technicaldocument);
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
