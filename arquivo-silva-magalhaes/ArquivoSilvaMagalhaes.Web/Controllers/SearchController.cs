﻿using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Controllers
{
    public static class PagedListAsyncExtensions
    {
        public async static Task<IPagedList<TModel>> ToPagedListAsync<TModel>(this IEnumerable<TModel> enumerable, int page, int count)
        {
            return await Task.Run(() => enumerable.ToPagedList(page, count));
        }
    }


    public class SearchController : Controller
    {
        private ArchiveDataContext db = new ArchiveDataContext();


        /// <summary>
        /// Forence lista paginada de elementos encontrados na pesquisa
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string query = "", int pageNumber = 1)
        {
            IEnumerable<TranslatedViewModel<Document, DocumentTranslation>> model = null;
            ViewBag.Query = query;

            if (query != "")
            {
                model = (await db.Documents
                    .Where(d => d.Title.Contains(query))
                    .Where(d => d.Collection.IsVisible)
                    .OrderBy(d => d.Id)
                    .ToListAsync())
                    .Select(d => new TranslatedViewModel<Document, DocumentTranslation>(d))
                    .ToPagedList(pageNumber, 10);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SearchResults", model);
            }

            return View(model);
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
