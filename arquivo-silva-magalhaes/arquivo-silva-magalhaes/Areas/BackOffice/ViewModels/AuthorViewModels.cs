using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Biography { get; set; }
        public string Curriculum { get; set; }

        public AuthorViewModel(Author author)
        {
            Name = String.Format("{0}, {1}", author.LastName, author.FirstName);
            BirthDate = author.BirthDate;

            Biography = author.Biography;
            Curriculum = author.Curriculum;
        }
    }

    public class AuthorEditModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public List<AuthorTextEditModel> AuthorTextEditModels { get; set; }

        [Required]
        public string NationalityPt { get; set; }
        [Required]
        public string BiographyPt { get; set; }
        [Required]
        public string CurriculumPt { get; set; }

        public string NationalityEn { get; set; }
        public string BiographyEn { get; set; }
        public string CurriculumEn { get; set; }
    }

    public class AuthorTextEditModel : IValidatableObject
    {
        public string LanguageCode { get; set; }
        public string DisplayLanguageName { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string Curriculum { get; set; }

        // TODO: I18n this.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nationality.Trim().Length == 0)
            {
                yield return new ValidationResult("Nacionalidade tem que ter conteúdo.");
            }
            if (Biography.Trim().Length == 0)
            {
                yield return new ValidationResult("Biografia tem que ter conteúdo.");
            }
            if (Curriculum.Trim().Length == 0)
            {
                yield return new ValidationResult("Curriculum tem que ter conteúdo.");
            }

        }
    }
}