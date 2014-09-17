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

        private List<PortalViewModel> GetPortalViewModel()
        {
            List<PortalViewModel> portalView = new List<PortalViewModel>();
            return (portalView);
        }

        public ActionResult Index()
        {
            return View(GetIndexViewModel());
        }

        private List<IndexViewModel> GetIndexViewModel()
        {
            List<IndexViewModel> indexView = new List<IndexViewModel>();
            foreach (var ind in db.Banners.ToList())
            {
                IndexViewModel index = new IndexViewModel();
                index.Id = ind.Id;
                index.UriPath = ind.UriPath;
                index.Caption = ind.Translations.LastOrDefault().Caption;
                var SpotlightVideo = db.SpotlightVideos.ToList();
                {
                    foreach (var v in db.SpotlightVideos.ToList())
                    {
                        index.Video = v.UriPath;
                    }
                }
                indexView.Add(index);
            }
            return indexView;
        }

        public ActionResult IndexView()
        {
            return View(db.BannerTranslations.ToList());
        }

        public ActionResult History()
        {
            return View(db.ArchiveTranslations.ToList());
        }

        public ActionResult Shop()
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