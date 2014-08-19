using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{


    public class ImageViewModel
    {
        public Image Image { get; set; }

        public List<SelectListItem> AvailableKeywords { get; set; }
        public List<SelectListItem> AvailableDocuments { get; set; }
    }

    public class ImageEditViewModel
    {
        public Image Image { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationErrorStrings), ErrorMessageResourceName = "MustChooseAtLeastOne")]
        [Display(ResourceType = typeof(ImageStrings), Name = "Keywords")]
        public List<SelectListItem> AvailableKeywords { get; set; }

        [Required]
        [Display(ResourceType = typeof(ImageStrings), Name = "Document")]
        public List<SelectListItem> AvailableDocuments { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationErrorStrings), ErrorMessageResourceName = "MustChooseAtLeastOne")]
        public int[] KeywordIds { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }
    }

    public class ImageCreateModel
    {
        [Display(ResourceType = typeof(DataStrings), Name = "Image__ProductionDate")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string ImageCode { get; set; }


    }
}