using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class CollectionViewModel
    {
    }

    public class CollectionEditViewModel
    {
        public int Id { get; set; }

        public int CollectionType { get; set; }
        public IEnumerable<SelectListItem> AvailableTypes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "ProductionDate")]
        public DateTime ProductionDate { get; set; }

        public bool HasAttachments { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "OrganizationType")]
        public string OrganizationSystem { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogCode")]
        public string CatalogCode { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }
    }

    public class CollectionI18nViewModel
    {
        public int CollectionId { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode { get; set; }
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Provenience")]
        public string Provenience { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "AdministrativeAndBiographicStory")]
        public string AdministrativeAndBiographicStory { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Dimension")]
        public string Dimension { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "FieldAndContents")]
        public string FieldAndContents { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "CopyrightInfo")]
        public string CopyrightInfo { get; set; }
    }
}