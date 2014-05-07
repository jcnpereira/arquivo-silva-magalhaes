using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public enum EventType : byte
    {
        //[Description("Exposição")]
        [Display()]
        Expo = 1,
        School = 2,
        Other = 100
    }

    public class EventEditViewModel : IValidatableObject
    {

        public int Id { get; set; }



        [Required]
        [MaxLength(50)]
        //[Display(ResourceType = typeof(DataStrings), Name = "Place")]
        public string Place { get; set; }


        [Required]
        [MaxLength(30)]
        //[Display(ResourceType = typeof(DataStrings), Name = "Coordinates")]
        public string Coordinates { get; set; }

        [Required]
        [MaxLength(30)]
        //[Display(ResourceType = typeof(DataStrings), Name = "VisitorInformation")]
        public string VisitorInformation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[Display(ResourceType = typeof(DataStrings), Name = "StartMoment")]
        public DateTime StartMoment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[Display(ResourceType = typeof(DataStrings), Name = "EndMoment")]
        public DateTime EndMoment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[Display(ResourceType = typeof(DataStrings), Name = "PublishDate")]
        public DateTime PublishDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[Display(ResourceType = typeof(DataStrings), Name = "ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public bool HideAfterExpiry { get; set; }

        [Required]
        //[Display(ResourceType = typeof(EventType), Name = "EventType")]
        public EventType EventType { get; set; }


        // public IEnumerable<SelectListItem> EventTypes { get; set; }

        //public Language LanguageCode { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndMoment.CompareTo(StartMoment) < 0)
            {
                yield return new ValidationResult(ErrorStrings.EndMomentEarlierThanStartMoment);
            }
            if (PublishDate.CompareTo(ExpiryDate) < 0)
            {
                yield return new ValidationResult(ErrorStrings.ExpiryDateEarlierThanPublishDate);
            }


        }
    }


}
