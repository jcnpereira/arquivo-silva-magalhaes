using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public enum DocumentType : byte
    {
        PDF = 1,
        Video = 2,
        Other = 100
    }

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