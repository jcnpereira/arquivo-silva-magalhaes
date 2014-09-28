﻿using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public class ShowcasePhotosController : Controller
    {
        /// <summary>
        /// Associa entidade ShowcasePhoto às traduções existentes
        /// </summary>
        private ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db;

        public ShowcasePhotosController()
            : this(new TranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation>()) { }

        public ShowcasePhotosController(ITranslateableRepository<ShowcasePhoto, ShowcasePhotoTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece uma fotografia em destaque por página colocando a mais recente na primeira página
        /// e as outras nas seguintes dor ordem descendente
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            return View((await db.Entities
                .Include(sp => sp.Image)
                .OrderByDescending(sp => sp.VisibleSince)
                .Where(sp => sp.VisibleSince <= DateTime.Now)
                .ToListAsync())
                .Select(b => new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>(b))
                .ToPagedList(pageNumber, 10));
        }

        /// <summary>
        /// Fornece o nome do Comentador e o seu email caso seja permitido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowcasePhoto showcasephoto = await db.Entities
                .Include(sp => sp.Image)
                .FirstOrDefaultAsync(sp => sp.Id == id);


            if (showcasephoto == null || showcasephoto.VisibleSince >= DateTime.Now)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<ShowcasePhoto, ShowcasePhotoTranslation>(showcasephoto));
        }

        /// <summary>
        /// Actualização à base de dados
        /// </summary>
        /// <param name="disposing"></param>
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
