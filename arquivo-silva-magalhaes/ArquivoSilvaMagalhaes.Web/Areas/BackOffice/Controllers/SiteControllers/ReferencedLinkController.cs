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
        private ITranslateableRepository<ReferencedLink, ReferencedLinkTranslation> db;

        public ReferencedLinkController()
            : this(new TranslateableRepository<ReferencedLink, ReferencedLinkTranslation>())
        {
        }

        public ReferencedLinkController(ITranslateableRepository<ReferencedLink, ReferencedLinkTranslation> db)
        {
            this.db = db;
        }

        // GET: /BackOffice/ReferencedLink/
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .OrderBy(rl => rl.Id)
                .ToListAsync())
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
            ReferencedLink referencedlink = await db.GetByIdAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }

            referencedlink.Translations = referencedlink.Translations.ToList();

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
                db.Add(referencedlink);
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
            ReferencedLink referencedlink = await db.GetByIdAsync(id);
            if (referencedlink == null)
            {
                return HttpNotFound();
            }
            return View(referencedlink);
        }

        // POST: /BackOffice/ReferencedLink/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ReferencedLink referencedlink)
        {
            if (ModelState.IsValid)
            {
                db.Update(referencedlink);

                foreach (var t in referencedlink.Translations)
                {
                    db.UpdateTranslation(t);
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
            ReferencedLink referencedlink = await db.GetByIdAsync(id);
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
            ReferencedLink referencedlink = await db.GetByIdAsync(id);
            db.Remove(referencedlink);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var referencedLink = await db.GetByIdAsync(id);

            if (referencedLink == null)
            {
                return HttpNotFound();
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                referencedLink.Translations.Select(t => t.LanguageCode).ToArray());

            return View(new ReferencedLinkTranslation
            {
                Id = referencedLink.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(ReferencedLinkTranslation translation)
        {
            var item = await db.GetByIdAsync(translation.Id);

            if (ModelState.IsValid)
            {
                item.Translations.Add(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(
                item.Translations.Select(t => t.LanguageCode).ToArray());

            return View(translation);
        }

        public async Task<ActionResult> DeleteTranslation(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode) || languageCode == LanguageDefinitions.DefaultLanguage)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            return View(tr);
        }

        [HttpPost]
        [ActionName("DeleteTranslation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTranslationConfirmed(int? id, string languageCode)
        {
            if (id == null || string.IsNullOrEmpty(languageCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tr = await db.GetTranslationAsync(id.Value, languageCode);

            if (tr == null)
            {
                return HttpNotFound();
            }

            await db.RemoveTranslationByIdAsync(id, languageCode);

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
