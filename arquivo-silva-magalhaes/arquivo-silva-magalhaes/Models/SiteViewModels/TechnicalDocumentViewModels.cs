using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteViewModels
{
    public class TechnicalDocumentEditViewModel
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "File")]
        public HttpPostedFileBase UploadedFile { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentType")]
        public DocumentType? DocumentType { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public String Language { get; set; }
    }
}