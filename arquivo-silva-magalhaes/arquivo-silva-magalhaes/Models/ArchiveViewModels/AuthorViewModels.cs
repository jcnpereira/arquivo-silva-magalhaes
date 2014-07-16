using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{

    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "BirthDate")]
        public DateTime BirthDate { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "DeathDate")]
        public DateTime DeathDate { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Nationality")]
        public string Nationality { get; set; }


        [Display(ResourceType = typeof(DataStrings), Name = "Biography")]
        public string Biography { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Curriculum")]
        public string Curriculum { get; set; }
    }

    /// <summary>
    /// To be used in Create and Edit views.
    /// </summary>
    public class AuthorEditViewModel : IValidatableObject
    {

        public AuthorEditViewModel()
        {

        }

        /// <summary>
        /// Creates a view model from the data of an author.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="languageCode"></param>
        /// <param name="availableLanguages"></param>
        public AuthorEditViewModel(
            Author a, 
            string languageCode = LanguageDefinitions.DefaultLanguage, 
            IEnumerable<string> availableLanguages = null)
        {
            availableLanguages = availableLanguages ?? new List<string>();

            Id = a.Id;
            FirstName = a.FirstName;
            LastName = a.LastName;

            BirthDate = a.BirthDate;
            DeathDate = a.DeathDate;

            Translations = a.Translations.Select(t => new AuthorTranslationEditViewModel
                {
                    AuthorId = a.Id,
                    LanguageCode = t.LanguageCode,
                    Nationality = t.Nationality,
                    Curriculum = t.Curriculum,
                    Biography = t.Biography,
                    AvailableLanguages = availableLanguages.Select(l => new SelectListItem
                    {
                        Text = l, // TODO: Get localized text.
                        Value = l
                    }).ToList()
                }).ToList();
        }

        public int Id { get; set; }

        /// <summary>
        /// The first name(s) of this author.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(ResourceType = typeof(DataStrings), Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name(s) of this author.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(ResourceType = typeof(DataStrings), Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// The date on which this author was born.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "BirthDate")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The date on which this author died.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "DeathDate")]
        public DateTime DeathDate { get; set; }

        /// <summary>
        /// Localized texts descibing the biography and other aspects of this
        /// author.
        /// </summary>
        public virtual List<AuthorTranslationEditViewModel> Translations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // The death date cannot be earlier than the birth date.
            if (DeathDate.CompareTo(BirthDate) < 0)
            {
                yield return new ValidationResult(ErrorStrings.DeathDateEarlierThanBirthDate);
            }
        }
    }

    /// <summary>
    /// To be used in Create and Edit views.
    /// </summary>
    public class AuthorTranslationEditViewModel
    {

        public int AuthorId { get; set; }

        [Required]
        public string LanguageCode { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }

        [Required]
        [MaxLength(50)]
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
    }
}