using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public class SpecimenEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogCode")]
        public string CatalogCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "AuthorCatalogCode")]
        public string AuthorCatalogationCode { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "HasMarksOrStamps")]
        public bool HasMarksOrStamps { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Indexation")]
        public string Indexation { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Document")]
        public int? DocumentId { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Process")]
        public int ProcessId { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Format")]
        public int FormatId { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Keywords")]
        public int[] KeywordIds { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Classifications")]
        public int[] ClassificationIds { get; set; }

        [Required]
        public IEnumerable<SelectListItem> AvailableDocuments { get; set; }

        [Required]
        public IEnumerable<SelectListItem> AvailableProcesses { get; set; }

        [Required]
        public IEnumerable<SelectListItem> AvailableFormats { get; set; }

        [Required]
        public IEnumerable<SelectListItem> AvailableClassfications { get; set; }

        [Required]
        public IEnumerable<SelectListItem> AvailableKeywords { get; set; }

        public List<SpecimenTranslationEditViewModel> Translations { get; set; }
    }


    // TODO: Implement this!
    public class SpecimenTranslationEditViewModel
    {
        public int SpecimenId { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Topic")]
        public string Topic { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "StateSimple")]
        public string SimpleStateDescription { get; set; }

        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "StateDetailed")]
        public string DetailedStateDescription { get; set; }

        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "InterventionDescription")]
        public string InterventionDescription { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Publication")]
        public string Publication { get; set; }

        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }

    public class SpecimenPhotoUploadModel
    {

        public int Id { get; set; }

        [Required]
        public int SpecimenId { get; set; }

        public List<PhotoUploadModelItem> Items { get; set; }

        public List<HttpPostedFileBase> Photos { get; set; }
    }

    public class PhotoUploadModelItem
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "ScanDate")]
        public DateTime ScanDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ScanProcess")]
        public string Process { get; set; }


        public string OriginalFileName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "CopyrightInfo")]
        public string CopyrightInfo { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        public bool IsToConsider { get; set; }
    }
}