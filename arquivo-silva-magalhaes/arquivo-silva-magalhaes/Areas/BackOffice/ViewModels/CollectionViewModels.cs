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
        public DateTime ProductionDate { get; set; }

        public bool HasAttachments { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string OrganizationSystem { get; set; }

        [Required]
        public string CatalogCode { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }
    }

    public class CollectionI18nViewModel
    {
        public int CollectionId { get; set; }

        [Required]
        public string LanguageCode { get; set; }
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Provenience { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string AdministrativeAndBiographicStory { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Dimension { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string FieldAndContents { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string CopyrightInfo { get; set; }
    }
}