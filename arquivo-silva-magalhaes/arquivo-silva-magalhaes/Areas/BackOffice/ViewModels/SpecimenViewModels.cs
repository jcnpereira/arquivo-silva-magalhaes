using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class SpecimenCreateModel
    {

        public SpecimenCreateModel()
        {
            I18nTexts = new List<SpecimenI18NModel>
            {
                new SpecimenI18NModel { LanguageCode = LanguageDefinitions.DefaultLanguage }
            };
        }

        [Required]
        public string CatalogCode { get; set; }

        public string AuthorCatalogationCode { get; set; }
        [Required]
        public bool HasMarksOrStamps { get; set; }
        [Required]
        public string Indexation { get; set; }
        [Required]
        public string Notes { get; set; }

        [Required]
        public int? DocumentId { get; set; }
        public IEnumerable<SelectListItem> AvailableDocuments { get; set; }

        [Required]
        public int ProcessId { get; set; }
        public IEnumerable<SelectListItem> AvailableProcesses { get; set; }

        [Required]
        public int FormatId { get; set; }
        public IEnumerable<SelectListItem> AvailableFormats { get; set; }

        [Required]
        public int[] ClassificationIds { get; set; }
        public IEnumerable<SelectListItem> AvailableClassfications { get; set; }


        [Required]
        public int[] KeywordIds { get; set; }
        public IEnumerable<SelectListItem> AvailableKeywords { get; set; }

        public List<SpecimenI18NModel> I18nTexts { get; set; }
    }


    // TODO: Implement this!
    public class SpecimenI18NModel
    {
        public int SpecimenId { get; set; }

        [Required]
        public string LanguageCode { get; set; }
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string SimpleStateDescription { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string DetailedStateDescription { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string InterventionDescription { get; set; }

        [Required]
        public string Publication { get; set; }
    }

    public class SpecimenPhotoUploadModel
    {

        public int Id { get; set; }

        [Required]
        public int SpecimenId { get; set; }

        public List<PhotoUploadModelItem> Items { get; set; }

        public List<HttpPostedFileBase> Photos { get; set; }
    }

    public class PhotoUploadModelItem : IValidatableObject
    {
        [Required]
        [Range(1, 31)]
        public int ScanDay { get; set; }

        [Required]
        [Range(1, 12)]
        public int ScanMonth { get; set; }

        [Required]
        public int ScanYear { get; set; }

        [Required]
        public string Process { get; set; }

        public string OriginalFileName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string CopyrightInfo { get; set; }

        public bool IsVisible { get; set; }

        public bool IsToConsider { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            // Check for a valid date.
            try
            {
                var d = new DateTime(ScanYear, ScanMonth, ScanDay);
            }
            catch (Exception)
            {
                errors.Add(new ValidationResult(ErrorStrings.InvalidDate));
            }

            return errors;
        }
    }
}