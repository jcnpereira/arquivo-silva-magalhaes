using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public enum EventTypeViewModels : byte
    {
        //[Description("Exposição")]
        [Display()]
        Expo = 1,
        School = 2,
        Other = 100
    }

    public class EventEditViewModel : IValidatableObject
    {
       // public List<SelectListItem> EventType;
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(ResourceType = typeof(DataStrings), Name = "Place")]
        public string Place { get; set; }


        [Required]
        [MaxLength(30)]
        [Display(ResourceType = typeof(DataStrings), Name = "Coordinates")]
        public string Coordinates { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(ResourceType = typeof(DataStrings), Name = "VisitorInformation")]
        public string VisitorInformation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "StartMoment")]
        public DateTime StartMoment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "EndMoment")]
        public DateTime EndMoment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "PublishDate")]
        public DateTime PublishDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "HideAfterExpiry")]
        public bool HideAfterExpiry { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "EventType")]
        public EventType EventType { get; set; }


        // public IEnumerable<SelectListItem> EventTypes { get; set; }

        //public Language LanguageCode { get; set; }

        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndMoment.CompareTo(StartMoment) < 0)
            {
                yield return new ValidationResult(ErrorStrings.EndMomentEarlierThanStartMoment);
            }
            if (ExpiryDate.CompareTo(PublishDate) < 0)
            {
                yield return new ValidationResult(ErrorStrings.ExpiryDateEarlierThanPublishDate);
            }


        }
       
      
        [Display(ResourceType = typeof(UiStrings), Name = "Language")]
        public string LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Heading")]
        public string Heading { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "SpotLight")]
        public string SpotLight { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "TextContent")]
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }

    }
    public class EventI18nPartialModels
    {
      /*  public EventI18nPartialModels()
        {
            LanguageCode = "pt";
        }*/
        [Key]
        public int Id { get; set; }
        [Required]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string SpotLight { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }
        
       // [ForeignKey("EventId")]
       // public virtual Event Event { get; set; }

    }
}
