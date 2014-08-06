using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ContentTypeAttribute : ValidationAttribute
    {
        public string ContentType { get; set; }


        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file != null)
            {
                return new Regex(ContentType).IsMatch(file.ContentType);
            }
            else
            {
                throw new ValidationException();
            }
        }

        //public override string FormatErrorMessage(string name)
        //{
        //    if (ErrorMessage != null)
        //    {
        //        return String.Format(ErrorMessage, name, ContentType);
        //    }

        //    return String.Format(ValidationErrorStrings.InvalidFileType, name, ContentType);
        //}
    }
}