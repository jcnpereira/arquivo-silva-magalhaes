using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.SiteViewModels
{


    public class PhotographViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "UriPath")]
        public string UriPath { get; set; }

        [Display(Name = "Photo:")]
        public HttpPostedFileBase Image { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "PublicationDate")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "RemovalDate")]
        public DateTime RemovalDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        public List<PhotographI18nViewModel> I18nTexts { get; set; }
    }

    public class PhotographI18nViewModel
    {
        public int PhotographId { get; set; }
        
        public string LanguageCode { get; set; }

        [Required]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "BannerTexts")]
        public string Title { get; set; }
    }
}

