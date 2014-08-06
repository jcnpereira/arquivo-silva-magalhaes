using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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

        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "text";
            builder.Attributes["data-provide"] = "datepicker";

            builder.Attributes["data-date-format"] = "yyyy-mm-dd";
            builder.Attributes["data-date-start-date"] = "1753-1-1";
            builder.Attributes["placeholder"] = "yyyy-mm-dd";
            builder.Attributes["data-date-language"] = Thread.CurrentThread.CurrentUICulture.Name;

            var name = ExpressionHelper.GetExpressionText(expression);

            var prefix = html.ViewData.TemplateInfo.HtmlFieldPrefix;

            if (String.IsNullOrEmpty(prefix))
            {
                builder.Attributes["name"] = name;
            }
            else
            {
                builder.Attributes["name"] = prefix + "." + name;
            }



            builder.GenerateId(builder.Attributes["name"]);

            var type = typeof(TModel);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var prop = metadata.ContainerType.GetProperty(metadata.PropertyName);

            //var prop = type.GetProperty(name);
            var required = prop.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
            var display = prop.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

            var value = metadata.Model;

            if (value != null)
            {
                if (value is Nullable<DateTime>)
                {
                    if ((value as Nullable<DateTime>).HasValue)
                    {
                        var notNullValue = value as Nullable<DateTime>;
                        var date = (DateTime)notNullValue.Value;

                        if (date.CompareTo(new DateTime(1753, 1, 1)) < 0)
                        {
                            builder.Attributes["value"] = "";
                        }
                        else
                        {
                            builder.Attributes["value"] = ((DateTime)value).ToString("yyyy-MM-dd");
                        }
                    }
                }
                else if (value is DateTime)
                {
                    var date = (DateTime)value;

                    if (date.CompareTo(new DateTime(1753, 1, 1)) < 0)
                    {
                        builder.Attributes["value"] = "1753-1-1";
                    }
                    else
                    {
                        builder.Attributes["value"] = ((DateTime)value).ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    builder.Attributes["value"] = value.ToString();
                }
            }


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

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Renders a partial view, taking into account the html
        /// input name prefixes, specified by the expression.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="partialViewName"></param>
        /// <returns></returns>
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string partialViewName)
        {

            string name = ExpressionHelper.GetExpressionText(expression);

            string oldPrefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix;

            // For nested layouts.
            if (oldPrefix != "")
            {
                name = oldPrefix + "." + name;
            }

            object model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new TemplateInfo
                {
                    HtmlFieldPrefix = name
                }
            };

            return helper.Partial(partialViewName, model, viewData);

        }
    }
}