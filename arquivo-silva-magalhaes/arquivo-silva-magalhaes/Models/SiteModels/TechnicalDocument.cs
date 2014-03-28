using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class TechnicalDocument
    {
        public string Title;
        public string Localization;
        public DateTime UploadedDate;
        public DateTime LastModificationDate;
        public String Format;
        public Enum DocumentType;
        public string Language;

    }
}