using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ContactsController : Controller
    {        

        private ArchiveDataContext db = new ArchiveDataContext();

        public ActionResult Index()
        {
            return View();
        }


        //Language
        public ActionResult SetLanguage(string lang, string returnUrl)
        {
            Response.SetCookie(new HttpCookie("lang", lang));

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: /Contacts/
        //public async Task<ActionResult> Index()
        //{
        //    var archivecontacts = db.ArchiveContacts.Include(c => c.Archive);
        //    return View(await archivecontacts.ToListAsync());
        //}

        // GET: /Contacts/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contact contact = await db.ArchiveContacts.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contact);
        //}

        //// GET: /Contacts/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ArchiveId = new SelectList(db.Archives, "Id", "Title");
        //    return View();
        //}

        //// POST: /Contacts/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include="Id,ArchiveId")] Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ArchiveContacts.Add(contact);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ArchiveId = new SelectList(db.Archives, "Id", "Title", contact.ArchiveId);
        //    return View(contact);
        //}

        //// GET: /Contacts/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contact contact = await db.ArchiveContacts.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ArchiveId = new SelectList(db.Archives, "Id", "Title", contact.ArchiveId);
        //    return View(contact);
        //}

        //// POST: /Contacts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include="Id,ArchiveId")] Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(contact).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ArchiveId = new SelectList(db.Archives, "Id", "Title", contact.ArchiveId);
        //    return View(contact);
        //}

        //// GET: /Contacts/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contact contact = await db.ArchiveContacts.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contact);
        //}

        //// POST: /Contacts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Contact contact = await db.ArchiveContacts.FindAsync(id);
        //    db.ArchiveContacts.Remove(contact);
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
