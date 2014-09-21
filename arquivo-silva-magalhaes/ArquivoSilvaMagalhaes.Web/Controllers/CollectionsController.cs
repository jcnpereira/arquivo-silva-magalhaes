using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using ArquivoSilvaMagalhaes.Common;
using PagedList;
using PagedList.Mvc;


namespace ArquivoSilvaMagalhaes.Controllers
{
    public class CollectionsController : Controller
    {
        /// <summary>
        /// Associa Entidade Collections às traduções existentes
        /// </summary>
        private ITranslateableRepository<Collection, CollectionTranslation> db;

        public CollectionsController()
            : this(new TranslateableRepository<Collection, CollectionTranslation>()) { }

        public CollectionsController(ITranslateableRepository<Collection, CollectionTranslation> db)
        {
            this.db = db;
        }

        /// <summary>
        /// Fornece lista de coleções paginada e ordenada por data de fim de produção por ordem descendente
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int pageNumber=1)
        {
            return View((await db.Entities
                            .Include(col => col.Authors)
                            .Where(col => col.IsVisible)
                            .OrderByDescending(col => col.EndProductionDate)
                            .ToListAsync())
                            .Select(col => new TranslatedViewModel<Collection, CollectionTranslation>(col))
                            .ToPagedList(pageNumber, 12));
        }
        /// <summary>
        /// Forece detalhes de uma determinda coleção
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.GetByIdAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<Collection, CollectionTranslation>(collection));
        }

        /// <summary>
        /// Faz corresponder o documento à sua coleção 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Docs(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = await db.GetByIdAsync(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(new TranslatedViewModel<Collection, CollectionTranslation>(collection));
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

