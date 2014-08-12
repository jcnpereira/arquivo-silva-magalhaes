using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{

    public class AuthorViewModel
    {
        public AuthorViewModel()
        {

        }

        public AuthorViewModel(Author a) : this(a, LanguageDefinitions.DefaultLanguage) { }

        public AuthorViewModel(Author a, string languageCode)
        {
            //Id = a.Id;
            //FirstName = a.FirstName;
            //LastName = a.LastName;

            //BirthDate = a.BirthDate;
            //DeathDate = a.DeathDate;

            //// var t = a.Translations.Find(at => at.LanguageCode == languageCode && at.AuthorId == Id);

            //Biography = t.Biography;
            //Curriculum = t.Curriculum;
            //Nationality = t.Nationality;
        }

        public int Id { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "LastName")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "BirthDate")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
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
    public class AuthorEditViewModel
    {
        public Author Author { get; set; }
    }

    /// <summary>
    /// To be used in Create and Edit views.
    /// </summary>
    public class AuthorTranslationEditViewModel
    {
        public AuthorTranslationEditViewModel()
        {

        }

        public AuthorTranslationEditViewModel(
            AuthorTranslation at, 
            string languageCode = null, 
            IEnumerable<string> availableLanguages = null)
        {
            availableLanguages = availableLanguages ?? new List<string>();

            AuthorId = at.AuthorId;
            LanguageCode = at.LanguageCode ?? languageCode ?? LanguageDefinitions.DefaultLanguage;
            AvailableLanguages = availableLanguages.Select(al => new SelectListItem
                {
                    Text = al,
                    Value = al
                }).ToList();

            Biography = at.Biography;
            Curriculum = at.Curriculum;
            Nationality = at.Nationality;
        }


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