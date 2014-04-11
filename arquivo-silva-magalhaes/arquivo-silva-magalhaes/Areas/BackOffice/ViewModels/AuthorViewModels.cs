using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class AuthorViewModel
    {
        
    }

    public class AuthorEditViewModel : IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name(s) of this author.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// The date on which this author was born.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// The date on which this author died.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime DeathDate { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [Required]
        public string Curriculum { get; set; }

        public AuthorI18nViewModel I18nModel { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DeathDate.CompareTo(BirthDate) < 0)
            {
                yield return new ValidationResult(ErrorStrings.DeathDateEarlierThanBirthDate);
            }
        }
    }

    public class AuthorI18nViewModel
    {
        public int AuthorId { get; set; }

        [Required]
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