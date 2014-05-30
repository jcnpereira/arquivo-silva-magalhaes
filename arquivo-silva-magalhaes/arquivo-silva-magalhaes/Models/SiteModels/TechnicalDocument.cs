using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public enum DocumentType : byte
    {
        [Display(Name = "Imagem")]
        Image = 1,
        [Display(Name = "Texto")]
        Text = 2,
        [Display(Name = "Vídeo")]
        Video = 3,
        [Display(Name = "Outro")]
        Other = 100
    }

    public class TechnicalDocument
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string UriPath { get; set; }
        [Required]
        public DateTime UploadedDate { get; set; }
        [Required]
        public DateTime LastModificationDate { get; set; }
        [Required]
        public String Format { get; set; }
        [Required]
        public DocumentType DocumentType { get; set; }
        [Required]
        public string Language { get; set; }

    }
}