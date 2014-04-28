using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class AuthorEditViewModel : IValidatableObject
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(ResourceType = typeof(DataStrings), Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name(s) of this author.
        /// </summary>
        [Required]
        [MaxLength(30)]
        [Display(ResourceType = typeof(DataStrings), Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// The date on which this author was born.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "BirthDate")]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// The date on which this author died.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "DeathDate")]
        public DateTime DeathDate { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Nationality")]
        public string Nationality { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Biography")]
        public string Biography { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Curriculum")]
        public string Curriculum { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DeathDate.CompareTo(BirthDate) < 0)
            {
                yield return new ValidationResult(ErrorStrings.DeathDateEarlierThanBirthDate);
            }
        }
    }

    public class AuthorI18nEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(UiStrings), Name = "Language")]
        public string LanguageCode { get; set; }
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Curriculum { get; set; }
    }
}