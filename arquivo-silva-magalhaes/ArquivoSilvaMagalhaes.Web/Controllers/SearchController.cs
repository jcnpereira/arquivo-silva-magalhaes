using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.ViewModels;
using PagedList;
using System.Collections.Generic;
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
        public async Task<ActionResult> Index(string searchTerm = null, int pageNumber = 1)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var model = new SearchResult
                {
                    Query = searchTerm
                };

                model.Results = await db.Documents
                    .Where(ct => ct.Title.Contains(searchTerm))
                    .OrderBy(ct => ct.Id)
                    .Select(ct => new SearchResultItem
                    {
                        Id = ct.Id,
                        Title = ct.Title
                    })
                    .ToPagedListAsync(pageNumber, 10);


                return View(model);
            }

            return View();
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
