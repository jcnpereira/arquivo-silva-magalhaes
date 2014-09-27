using ArquivoSilvaMagalhaes.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.Controllers.SiteControllers
{
    public class SpotLightVideoController : SiteControllerBase
    {
        private IRepository<AppConfiguration> db;

        public SpotLightVideoController()
            : this(new GenericDbRepository<AppConfiguration>()) { }

        public SpotLightVideoController(IRepository<AppConfiguration> db)
        {
            this.db = db;
        }

        // GET: /BackOffice/SpotLightVideo/
        public async Task<ActionResult> Index()
        {
            return View(await db.GetByIdAsync(AppConfiguration.VideoUrlKey));
        }

        // GET: /BackOffice/SpotLightVideo/Edit/5
        public async Task<ActionResult> Edit()
        {
            return View(await db.GetByIdAsync(AppConfiguration.VideoUrlKey));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AppConfiguration configuration)
        {
            if (ModelState.IsValid)
            {
                var val = "";

                if (configuration.Value.Contains("?v="))
                {
                    val = configuration
                        .Value
                        .Split(new string[] { "?v=" }, System.StringSplitOptions.RemoveEmptyEntries)[1];
                }

                if (val.Contains("&"))
                {
                    val = val.Split('&')[0];
                }

                var config = new AppConfiguration
                {
                    Key = AppConfiguration.VideoUrlKey,
                    Value = val
                };
                db.Update(config);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(configuration);
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
