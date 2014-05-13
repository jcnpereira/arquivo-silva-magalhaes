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
        public string Title { get; set; }
        public string UriPath { get; set; }
        public DateTime UploadedDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public String Format { get; set; }
        public DocumentType DocumentType { get; set; }
        public string Language { get; set; }

    }
}