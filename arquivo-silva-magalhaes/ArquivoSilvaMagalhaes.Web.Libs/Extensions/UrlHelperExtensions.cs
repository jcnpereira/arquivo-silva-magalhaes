using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ArquivoSilvaMagalhaes.Web.Libs.Extensions
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Adds the current query string to the route value dictionary.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="values"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static RouteValueDictionary AddQueryStringToRoute(this UrlHelper helper, RouteValueDictionary values, HttpRequestBase request, object additionalProperties = null)
        {
            var rvd = new RouteValueDictionary(values);

            foreach (string key in request.QueryString.Keys)
            {
                rvd[key] = request.QueryString[key].ToString();
            }

            if (additionalProperties != null)
            {
                var t = additionalProperties.GetType();

                foreach (var prop in t.GetProperties())
                {
                    rvd[prop.Name] = prop.GetValue(additionalProperties);
                }
            }

            return rvd;
        }
    }
}
