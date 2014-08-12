using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    /// <summary>
    /// Encodes 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class HtmlEncodeAttribute : ValidationAttribute
    {
        public HtmlEncodeAttribute()
        {
            ThrowOnForbidden = true;
        }

        /// <summary>
        /// A comma-separated list of tags which are to be
        /// encoded.
        /// </summary>
        public string ForbiddenTags { get; set; }

        /// <summary>
        /// If true a RequestValidationException
        /// is thrown if a forbidden tag is found.
        /// 
        /// Otherwise, the tag is simply html-encoded.
        /// 
        /// Defaults to true.
        /// </summary>
        public bool ThrowOnForbidden { get; set; }

        public string AllowedTags { get; set; }

        public override bool IsValid(object value)
        {
            if (value is string)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(value as string);

                var elementsToRemove = new List<HtmlNode>();

                foreach (var tag in doc.DocumentNode.Descendants("script"))
                {
                    elementsToRemove.Add(tag);
                }

                foreach (var node in elementsToRemove)
                {
                    node.Remove();
                }

                var sw = new StringWriter();

                doc.Save(sw);

                sw.Flush();
                var outHtml = sw.ToString();

                sw.Close();
                value = outHtml;
            }
            return true;

            //string html = value as string;

            //if (String.IsNullOrEmpty(html))
            //{
            //    return true;
            //}
            //else
            //{
            //    ForbiddenTags = String.IsNullOrEmpty(ForbiddenTags) ? "" : ForbiddenTags;
            //    AllowedTags = String.IsNullOrEmpty(AllowedTags) ? "" : AllowedTags;

            //    if (!String.IsNullOrEmpty(ForbiddenTags))
            //    {
            //        var forbidden = ForbiddenTags.Split(',');
            //        var allowed = AllowedTags.Split(',');

            //        foreach (var tag in forbidden)
            //        {
            //            if (html.Contains("<" + tag + ">"))
            //            {
            //                if (ThrowOnForbidden)
            //                {
            //                    throw new HttpRequestValidationException();
            //                }
            //                else
            //                {
            //                    html = html
            //                        .Replace("<" + tag + ">", "&lt;" + tag + "&gt;")
            //                        .Replace("</" + tag + ">", "&lt;/" + tag + "&gt;");
            //                }
            //            }
            //        }
            //    }
            //}

            //return base.IsValid(value);
        }

    }
}