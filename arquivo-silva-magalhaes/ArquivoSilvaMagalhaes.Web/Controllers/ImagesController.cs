using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Threading.Tasks;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using PagedList.Mvc;
using ArquivoSilvaMagalhaes.ViewModels;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ImagesController : Controller
    {
        /// <summary>
        /// Associa Entidade Author às traduções existentes
        /// </summary>
         private ITranslateableRepository<Image, ImageTranslation> db;

        public ImagesController()
            : this(new TranslateableRepository<Image, ImageTranslation>()) { }

        public ImagesController(ITranslateableRepository<Image, ImageTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece lista paginada de imagens correspondentes ao documento onde estão inseridas 
        /// e ordenada pelo id da imagem
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int? id, int pageNumber=1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image img = new Image();

            return View((await db.Entities
                .Include(i => i.Translations)
                .Where(i => i.IsVisible)
                .Where(i => i.DocumentId == id)
                .OrderBy(i => i.Id)
                .ToListAsync())
                .Select(doc => new TranslatedViewModel<Image, ImageTranslation>(doc))
                .ToPagedList(pageNumber, 12));
        }


       /// <summary>
       /// Forence os detalhes de determinada imagem
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.GetByIdAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<Image, ImageTranslation>(image));
        }

        /// <summary>
        /// Actualização à base de dados
        /// </summary>
        /// <param name="disposing"></param>
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
