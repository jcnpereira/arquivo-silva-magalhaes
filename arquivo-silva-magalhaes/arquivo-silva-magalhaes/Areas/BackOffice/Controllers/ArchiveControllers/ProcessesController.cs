using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers
{
    public class ProcessesController : BackOfficeController
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        // GET: BackOffice/Processes
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View(await Task.Run(() => db.Processes
                .OrderBy(p => p.Id)
                .ToPagedList(pageNumber, 10)));
        }

        // GET: BackOffice/Processes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process p = await db.Processes.FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // GET: BackOffice/Processes/Create
        public ActionResult Create()
        {
            var model = new ProcessTranslation
            {
                LanguageCode = LanguageDefinitions.DefaultLanguage
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProcessFields", model);
            }

            return View(model);
        }

        // POST: BackOffice/Processes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProcessTranslation pt)
        {
            if (ModelState.IsValid)
            {
                var process = new Process();
                process.Translations.Add(pt);

                db.Processes.Add(process);
                await db.SaveChangesAsync();

                if (Request.IsAjaxRequest())
                {
                    var result = new List<object>
                    {
                        new {
                            text = UiPrompts.ChooseOne,
                            value = ""
                        }
                    };

                    result.AddRange(db.ProcessTranslations
                          .Where(ptr => ptr.LanguageCode == LanguageDefinitions.DefaultLanguage)
                          .OrderBy(ptr => ptr.ProcessId)
                          .Select(ptr => new
                          {
                              text = ptr.Value,
                              value = ptr.ProcessId.ToString()
                          }));

                    return Json(result);
                }

                return RedirectToAction("Index");
            }

            return View(pt);
        }

        // GET: BackOffice/Processes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process process = await db.Processes.FindAsync(id);

            if (process == null)
            {
                return HttpNotFound();
            }
            process.Translations = process.Translations.ToList();
            return View(process);
        }

        // POST: BackOffice/Processes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Process process)
        {
            if (ModelState.IsValid)
            {
                foreach (var translation in process.Translations)
                {
                    db.Entry(translation).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(process);
        }

        // GET: BackOffice/Processes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Process p = await db.Processes.FindAsync(id);

            if (p == null)
            {
                return HttpNotFound();
            }
            p.Translations = p.Translations.ToList();
            return View(p);
        }

        // POST: BackOffice/Processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Process process = await db.Processes.FindAsync(id);
            db.Processes.Remove(process);
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
