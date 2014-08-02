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

        public static MvcHtmlString FileUploadFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "file";
            builder.Attributes["name"] = name;

            var type = typeof(TModel);

            var prop = type.GetProperty(name);
            var required = prop.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
            var display = prop.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;


            if (required != null)
            {
                if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
                {
                    builder.Attributes["data-val"] = "true";

                    builder.Attributes["data-val-required"] = 
                        required.FormatErrorMessage(display != null ? display.GetName() : name);
                }
                else
                {
                    builder.Attributes["required"] = "";
                }
            }

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

                foreach (var key in attributes.Keys)
                {
                    builder.Attributes[key] = attributes[key].ToString();
                }
            }

            builder.GenerateId(name);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}