using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    public static class ViewExtensions
    {
        /// <summary>
        /// Returns an HTML-encoded string for a model whose property
        /// contains line breaks.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayMultilineFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model).Replace("\r\n", "<br />\r\n");

            if (String.IsNullOrEmpty(model))
            {
                return MvcHtmlString.Empty;
            }

            return MvcHtmlString.Create(model);
        }
    }
}