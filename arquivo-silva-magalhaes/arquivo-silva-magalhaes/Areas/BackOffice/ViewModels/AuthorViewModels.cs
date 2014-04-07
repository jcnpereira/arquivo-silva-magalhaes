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
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DeathDate { get; set; }

        public string Nationality { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
        [DataType(DataType.MultilineText)]
        public string Curriculum { get; set; }

        public AuthorViewModel(Author author)
        {
            Id = author.Id;
            Name = String.Format("{0}, {1}", author.LastName, author.FirstName);
            BirthDate = author.BirthDate;
            DeathDate = author.DeathDate;

            Biography = author.Biography;
            Curriculum = author.Curriculum;
            Nationality = author.Nationality;
        }
    }

    public class AuthorEditModel
    {
        public AuthorEditModel()
        {
            BirthDate = new DateTime(1800, 1, 1);
            // DeathDate = new DateTime(1850, 1, 1);
        }

        public AuthorEditModel(Author author)
        {
            FirstName = author.FirstName;
            LastName = author.LastName;

            BirthDate = author.BirthDate;
            DeathDate = author.DeathDate;

            var textPt = author.AuthorTexts.First(t => t.LanguageCode == "pt");
            AuthorText textEn = null;
            try
            {
                textEn = author.AuthorTexts.First(t => t.LanguageCode == "en");
            }
            catch (Exception) { }

            NationalityPt = textPt.Nationality;
            BiographyPt = textPt.Biography;
            CurriculumPt = textPt.Curriculum;

            if (textEn != null)
            {
                NationalityEn = textEn.Nationality;
                BiographyEn = textEn.Biography;
                CurriculumEn = textEn.Curriculum;
            }
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        [Required]
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DeathDate { get; set; }

        // public List<AuthorTextEditModel> AuthorTextEditModels { get; set; }

        // Properties for the Portuguese language.
        [Required]
        public string NationalityPt { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string BiographyPt { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string CurriculumPt { get; set; }

        public string NationalityEn { get; set; }
        [DataType(DataType.MultilineText)]
        public string BiographyEn { get; set; }
        [DataType(DataType.MultilineText)]
        public string CurriculumEn { get; set; }
    }

    public class AuthorTextEditModel// : IValidatableObject
    {
        public int AuthorId { get; set; }
        public string LanguageCode { get; set; }
        public string DisplayLanguageName { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string Curriculum { get; set; }

        //// TODO: I18n this.
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Nationality.Trim().Length == 0)
        //    {
        //        yield return new ValidationResult("Nacionalidade tem que ter conteúdo.");
        //    }
        //    if (Biography.Trim().Length == 0)
        //    {
        //        yield return new ValidationResult("Biografia tem que ter conteúdo.");
        //    }
        //    if (Curriculum.Trim().Length == 0)
        //    {
        //        yield return new ValidationResult("Curriculum tem que ter conteúdo.");
        //    }
        //}
    }
}