using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class ReferencedLinkController : SiteControllerBase
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: /BackOffice/ReferencedLink/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.ReferencedLinks.ToListAsync())
                .Select(l => new TranslatedViewModel<ReferencedLink, ReferencedLinkTranslation>(l))
                .ToPagedList(pageNumber, 10));
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
            var link = new ReferencedLink();

            link.Translations.Add(new ReferencedLinkTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

            return View(link);
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

                foreach (var t in referencedlink.Translations)
                {
                    db.Entry(t).State = EntityState.Modified;
                }

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
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
