using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.SiteViewModels;
using ArquivoSilvaMagalhaes.Utilitites;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ShowcasePhotoesController : BackOfficeController
    {
        private ArchiveDataContext _db = new ArchiveDataContext();

        // GET: BackOffice/ShowcasePhotoes
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => 
                _db.ShowcasePhotoes
                   .OrderBy(s => s.Id)
                   .ToPagedList(pageNumber, 10)));
        }

        // GET: BackOffice/ShowcasePhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            return View(showcasePhoto);
        }

        // GET: BackOffice/ShowcasePhotoes/Create
        public ActionResult Create()
        {
            var photo = new ShowcasePhoto();
            photo.Translations.Add(new ShowcasePhotoTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

            return View(GenerateViewModel(photo));
        }

        // POST: BackOffice/ShowcasePhotoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShowcasePhoto showcasePhoto)
        {
            if (ModelState.IsValid)
            {
                _db.ShowcasePhotoes.Add(showcasePhoto);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(showcasePhoto));
        }

        // GET: BackOffice/ShowcasePhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            return View(GenerateViewModel(showcasePhoto));
        }

        // POST: BackOffice/ShowcasePhotoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShowcasePhoto showcasePhoto)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(showcasePhoto).State = EntityState.Modified;

                foreach (var item in showcasePhoto.Translations)
                {
                    _db.Entry(item).State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(GenerateViewModel(showcasePhoto));
        }

        // GET: BackOffice/ShowcasePhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            if (showcasePhoto == null)
            {
                return HttpNotFound();
            }
            return View(showcasePhoto);
        }

        // POST: BackOffice/ShowcasePhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShowcasePhoto showcasePhoto = await _db.ShowcasePhotoes.FindAsync(id);
            _db.ShowcasePhotoes.Remove(showcasePhoto);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private ShowcasePhotoEditViewModel GenerateViewModel(ShowcasePhoto photo)
        {
            var model = new ShowcasePhotoEditViewModel
            {
                ShowcasePhoto = photo
            };

            model.AvailableImages = _db.ImageTranslations.Select(d => new SelectListItem
                {
                    Value = d.ImageId.ToString(),
                    Text = d.Title
                });

            return model;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
