using ArquivoSilvaMagalhaes.Models;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class HomeController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contactos()
        {
            return View();
        }


        public ActionResult Acervo()
        {
            return View();
        }

        public ActionResult Destaque()
        {
            return View();
        }

        public ActionResult Documentos()
        {
            return View();
        }

        public ActionResult Eventos()
        {
            return View();
        }

        public ActionResult Historia()
        {
            return View();
        }

        public ActionResult Links()
        {
            
            //var links = db.ReferencedLinks.ToList();
            
            return View(db.ReferencedLinks);

            //var links = from l in db.ReferencedLinks
            //            select new ReferencedLink
            //         {
            //             Id=l.Id,
            //             Title = l.Title,
            //             Link = l.Link,
            //             DateOfCreation=l.DateOfCreation,
            //             LastModifiedDate=l.LastModifiedDate,
            //             Description=l.Description,
            //             IsUsefulLink=l.IsUsefulLink,
            //             EventsUsingThis=l.EventsUsingThis,
            //             NewsUsingThis=l.NewsUsingThis
            //         };

            //return View(links);
            //return View();
        }

        public ActionResult Loja()
        {
            return View();
        }

        public ActionResult Noticias()
        {
            return View();
        }

        public ActionResult Pesquisa()
        {
            return View();
        }

        public ActionResult Projetos()
        {
            return View();
        }


    }
}