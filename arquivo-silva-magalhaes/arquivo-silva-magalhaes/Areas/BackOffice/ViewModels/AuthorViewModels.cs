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
        public AuthorEditModel()
        {
            LanguageCode = "pt";
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public string LanguageCode { get; set; }

        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string Curriculum { get; set; }
    }

    public class AuthorTextEditModel
    {
        public string LanguageCode { get; set; }
        [Required]
        public string Curriculum { get; set; }
        [Required]
        public string Biography { get; set; }
        [Required]
        public string Nationality { get; set; }
    }
}