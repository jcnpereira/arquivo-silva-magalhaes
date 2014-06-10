using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public partial class CollectionEditViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// The type of this collection.
        /// </summary>
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionType"), Required]
        public CollectionType? Type { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        [Required, DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "ProductionDate")]
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        [Display(ResourceType = typeof(DataStrings),Name ="LogoLocation")]
        public string LogoLocation { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "HasAttachments")]
        public bool HasAttachments { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "OrganizationSystem")]
        public string OrganizationSystem { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Code used by the archive to physically catalog this
        /// collection.
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogCode")]
        public string CatalogCode { get; set; }


        [NotMapped]
        public int[] AuthorIds { get; set; }

        [Required]
        [Display(Name = "Autores desta coleção")]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }

        [NotMapped]
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionLogo")]
        public HttpPostedFileBase Logo { get; set; }

        public List<CollectionTranslationEditViewModel> Translations { get; set; }
    }

    public partial class CollectionTranslationEditViewModel
    {
        public int CollectionId { get; set; }

        [Required]
        public string LanguageCode { get; set; }

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
        [Display(ResourceType = typeof(DataStrings), Name = "Dimension")]
        public string Dimension { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "FieldAndContents")]
        public string FieldAndContents { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CopyrightInfo")]
        public string CopyrightInfo { get; set; }

        [Required, NotMapped]
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}