using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// Returns an Html string containing the localized
        /// value for an enum, if the enum entry has a Display attribute.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString EnumDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) 
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = metadata.Model;

            var field = model.ToString();

            var display = ((DisplayAttribute[])model.GetType().GetField(field).GetCustomAttributes(typeof(DisplayAttribute), false)).FirstOrDefault();

            var result = "";

            if (display != null)
            {
                result = display.GetName();
            }
            else
            {
                result = field;
            }

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString FileUploadFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string acceptedFileType = "*", bool multiple = false, object htmlAttributes = null)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "file";
            builder.Attributes["accept"] = acceptedFileType;
            builder.Attributes["name"] = name;
            if (multiple)
            {
                builder.Attributes["multiple"] = "multiple";
            }

            if (htmlAttributes != null)
            {
                foreach (var property in htmlAttributes.GetType().GetProperties())
                {
                    builder.Attributes[property.Name] = property.GetValue(htmlAttributes).ToString();
                }
            }

            builder.GenerateId(name);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}