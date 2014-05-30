using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{

    public enum DocumentType : byte
    {
        [Display(Name = "Imagem")]
        Image = 1,
        [Display(Name = "Texto")]
        Text = 2,
        [Display(Name = "Vídeo")]
        Video =3,
        [Display(Name = "Outro")]
        Other = 100
    }


    public class TechnicalDocumentViewModels
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "UriPath")]
        //public HttpPostedFileBase UriPath { get; set; }
        public string UriPath { get; set; }
        
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "UploadedDate")]
        [DataType(DataType.Date)]
        public DateTime UploadedDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LastModificationDate")]
        [DataType(DataType.Date)]
        public DateTime LastModificationDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Format")]
        public string Format { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentType")]
        public DocumentType DocumentType { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public String Language { get; set; }
    }
}