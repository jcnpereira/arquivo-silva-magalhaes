using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;
using System.Threading.Tasks;

namespace ArquivoSilvaMagalhaes.Common
{
    public static class AsyncPagedListExtensions
    {
        public static Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            return Task.Run(() => superset.ToPagedList(pageNumber, pageSize));
        }
    }
}
