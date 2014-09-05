using ArquivoSilvaMagalhaes.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Threading;
using ArquivoSilvaMagalhaes.ViewModels;
using System.Web;
using System.Threading.Tasks;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using PagedList;
using PagedList.Mvc;
using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.Collections.Generic;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class HomeController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();

        public ActionResult About()
        {
            return View();
        }

        public ActionResult IndexView()
        {
            IndexViewModel viewModel = new IndexViewModel();
            List<IndexViewModel> viewModels = new List<IndexViewModel>();
            //viewModel.BannerTranslation = Get the data from Service;
            //viewModel.Video = Get the data from Service;
            //var model= db.BannerTranslations.Include(c =>c.)
            return View(viewModels);
        }


        public ActionResult Index()
        {
            return View(db.BannerTranslations.ToList());
        }

        public ActionResult History()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

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

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Links(int pageNumber = 1)
        {
            var model = db.ReferencedLinks.ToList().ToPagedList(pageNumber, 10);
            return View(model);
            //return View(db.ReferencedLinks.ToList());
        }

        public ActionResult Collections()
        {
            var model = db.Collections
                .Where(c => c.IsVisible)
                .Include(c => c.Translations)
                .ToList()
                .Select(c => new TranslatedViewModel<Collection, CollectionTranslation>(c))
                .ToList();
            return View(model);
        }

        public async Task<ActionResult> Partnerships()
        {
            return View(await db.Partnerships.ToListAsync());
        }
    }
}