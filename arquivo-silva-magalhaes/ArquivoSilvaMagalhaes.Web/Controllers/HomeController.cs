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

        /// <summary>
        /// Página de about
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }


        /// <summary>
        /// Fornece ViewModel que agrega atributos de BannerModel e SpotLightVideoModel
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(GetIndexViewModel());
        }

        /// <summary>
        /// Obtem os atributos de Banner e Video
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Fornece os atributos listados contidos no model BannerTranslation
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexView()
        {
            return View(db.BannerTranslations.ToList());
        }

        /// <summary>
        /// Forence os atributos listados contidos no model ArchiveTranslations
        /// </summary>
        /// <returns></returns>
        public ActionResult History()
        {
            return View(db.ArchiveTranslations.ToList());
        }
        

        /// <summary>
        /// Loja ainda não existe (poderá ser desenvolvida futuramente)
        /// </summary>
        /// <returns></returns>
        //public ActionResult Shop()
        //{
        //    return View();
        //}

        /// <summary>
        /// Estabelece qual a lingua a ser usada no portal
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Página de contactos estática
        /// </summary>
        /// <returns></returns>
        public ActionResult Contacts()
        {
            return View();
        }

        /// <summary>
        /// Fornece a lista de links existentes paginada
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ActionResult Links(int pageNumber = 1)
        {
            var model = db.ReferencedLinks.ToList().ToPagedList(pageNumber, 10);
            return View(model);
        }

        /// <summary>
        /// Forence a lista de coleções
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Forence a lista de parcerias
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Partnerships()
        {
            return View(await db.Partnerships.ToListAsync());
        }
    }
}