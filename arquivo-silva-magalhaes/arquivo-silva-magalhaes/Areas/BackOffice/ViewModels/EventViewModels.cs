using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class EventEditViewModels : IValidatableObject
    {
        
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
            public DateTime VisitorInformation { get; set; }
           
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
            public bool HideAfterExpiry { get; set; }

            [Required]
            [Display(ResourceType = typeof(UiStrings), Name = "EventType")]
            public string LanguageCode { get; set; }
            public IEnumerable<SelectListItem> AvailableEvents { get; set; }    



            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (EndMoment.CompareTo(StartMoment) < 0)
                {
                    yield return new ValidationResult(ErrorStrings.DeathDateEarlierThanBirthDate);
                }
                if (PublishDate.CompareTo(ExpiryDate) < 0)
                {
                    yield return new ValidationResult(ErrorStrings.DeathDateEarlierThanBirthDate);
                }
            }
        }

      
    }
