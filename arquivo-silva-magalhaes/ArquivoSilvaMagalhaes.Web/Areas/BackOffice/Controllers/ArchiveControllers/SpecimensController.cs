using ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels;
using ArquivoSilvaMagalhaes.Common;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Web.I18n;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.ArchiveControllers
{
    public class SpecimensController : ArchiveControllerBase
    {
        private ITranslateableRepository<Specimen, SpecimenTranslation> db;

        public SpecimensController()
            : this(new TranslateableRepository<Specimen, SpecimenTranslation>()) { }

        public SpecimensController(ITranslateableRepository<Specimen, SpecimenTranslation> db)
        {
            this.db = db;
        }

        // GET: BackOffice/Specimens
        public ActionResult Index(int imageId = 0, int formatId = 0, int processId = 0, int pageNumber = 1, string query = "")
        {
            var model = db.Entities
                    .Where(s =>
                        (query == "" || s.ArchivalReferenceCode.Contains(query)) &&
                        (imageId == 0 || s.ImageId == imageId) &&
                        (formatId == 0 || s.FormatId == formatId) &&
                        (processId == 0 | s.ProcessId == processId))
                    .OrderBy(s => new { s.ImageId, s.Id })
                    .ToPagedList(pageNumber, 10);

            ViewBag.Query = query;
            ViewBag.FormatId = formatId;
            ViewBag.ProcessId = processId;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model);
            }

            return View(model);
        }

        // GET: BackOffice/Specimens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await db.GetByIdAsync(id);

            if (specimen == null)
            {
                return HttpNotFound();
            }

            specimen.DigitalPhotographs = specimen.DigitalPhotographs.ToList();

            return View(specimen);
        }

        // GET: BackOffice/Specimens/Create
        public async Task<ActionResult> Create(int imageId = 0)
        {
            var s = new Specimen();

            if (imageId > 0)
            {
                var image = await db.Set<Image>().FirstOrDefaultAsync(i => i.Id == imageId);

                if (image != null)
                {
                    s.ImageId = image.Id;

                    var doc = image.Document;
                    var collection = doc.Collection;

                    s.ReferenceCode = CodeGenerator.SuggestSpecimenCode(imageId);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ErrorStrings.Specimen__UnknownImage);
                }
            }

            s.Translations.Add(new SpecimenTranslation
                {
                    LanguageCode = LanguageDefinitions.DefaultLanguage
                });

            return View(GenerateViewModel(s));
        }

        // POST: BackOffice/Specimens/Create To protect from overposting attacks, please enable the
        // specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Specimen specimen)
        {
            if (DoesCodeAlreadyExist(specimen))
            {
                ModelState.AddModelError("ReferenceCode", SpecimenStrings.CodeAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                db.Add(specimen);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(specimen));
        }

        // GET: BackOffice/Specimens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await db.GetByIdAsync(id);
            if (specimen == null)
            {
                return HttpNotFound();
            }

            return View(GenerateViewModel(specimen));
        }

        // POST: BackOffice/Specimens/Edit/5 To protect from overposting attacks, please enable the
        // specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Specimen specimen)
        {
            if (DoesCodeAlreadyExist(specimen))
            {
                ModelState.AddModelError("ReferenceCode", SpecimenStrings.CodeAlreadyExists);
            }

            if (ModelState.IsValid)
            {
                db.Update(specimen);

                foreach (var t in specimen.Translations)
                {
                    db.UpdateTranslation(t);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(GenerateViewModel(specimen));
        }

        // GET: BackOffice/Specimens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specimen specimen = await db.GetByIdAsync(id);
            if (specimen == null)
            {
                return HttpNotFound();
            }
            return View(specimen);
        }

        // POST: BackOffice/Specimens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Specimen specimen = await db.GetByIdAsync(id);
            db.Remove(specimen);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ListPhotos(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var s = await db.GetByIdAsync(id);

            if (s == null) { return HttpNotFound(); }

            ViewBag.Id = s.Id;

            return View(s.DigitalPhotographs.ToList());
        }

        #region Translation Actions
        public async Task<ActionResult> AddTranslation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var image = await db.GetByIdAsync(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            var model = new SpecimenTranslation
            {
                SpecimenId = image.Id
            };

            ViewBag.Languages = LanguageDefinitions.GenerateAvailableLanguageDDL(image.Translations.Select(t => t.LanguageCode));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTranslation(SpecimenTranslation translation)
        {
            if (ModelState.IsValid)
            {
                db.AddTranslation(translation);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

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
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private SpecimenEditViewModel GenerateViewModel(Specimen specimen)
        {
            var model = new SpecimenEditViewModel();

            model.AvailableImages.Add(new SelectListItem
                {
                    Value = "",
                    Text = UiPrompts.ChooseOne,
                    Selected = true
                });

            model.AvailableImages.AddRange(
                db.Set<Image>()
                   .OrderBy(i => i.Id)
                   .Select(i => new SelectListItem
                   {
                       Value = i.Id.ToString(),
                       Text = i.ImageCode,
                       Selected = specimen.ImageId == i.Id
                   }));

            model.AvailableProcesses.Add(new SelectListItem
            {
                Value = "",
                Text = UiPrompts.ChooseOne,
                Selected = true
            });

            model.AvailableProcesses.AddRange(
                db.Set<ProcessTranslation>()
                   .Where(pt => pt.LanguageCode == LanguageDefinitions.DefaultLanguage)
                   .OrderBy(pt => pt.ProcessId)
                   .Select(pt => new SelectListItem
                {
                    Value = pt.ProcessId.ToString(),
                    Text = pt.Value,
                    Selected = specimen.ProcessId == pt.ProcessId
                }));

            model.AvailableFormats.Add(new SelectListItem
            {
                Value = "",
                Text = UiPrompts.ChooseOne,
                Selected = true
            });

            model.AvailableFormats.AddRange(db.Set<Format>()
                .OrderBy(f => f.Id)
                .Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = f.FormatDescription,
                    Selected = specimen.FormatId == f.Id
                }));

            model.Specimen = specimen;

            return model;
        }

        private bool DoesCodeAlreadyExist(Specimen s)
        {
            return db.Entities
                .Any(d => d.ReferenceCode == s.ReferenceCode && d.Id != s.Id);
        }

        public async Task<ActionResult> SuggestCode(int? imageId)
        {
            if (imageId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var i = await db.Set<Image>().FirstOrDefaultAsync(img => img.Id == imageId);

            if (i == null)
            {
                return HttpNotFound();
            }

            return Json(CodeGenerator.SuggestSpecimenCode(i.Id), JsonRequestBehavior.AllowGet);
        }
    }
}