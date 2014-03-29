using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class TechnicalDocument
    {
        [Key]
        public int Id { get; set; }
        public string Title;
        public string UriPath;
        public DateTime UploadedDate;
        public DateTime LastModificationDate;
        public String Format;
        public Enum DocumentType;
        public string Language;

    }
}