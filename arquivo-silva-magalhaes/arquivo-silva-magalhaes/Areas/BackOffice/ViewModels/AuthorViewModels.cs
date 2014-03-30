using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System;
using System.Collections.Generic;
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

        public AuthorViewModel(Author author, string locale)
        {
            Name = String.Format("{0}, {1}", author.LastName, author.FirstName);
            BirthDate = author.BirthDate;

            var localizedText = author.AuthorTexts.FirstOrDefault(t => locale.Contains(t.LanguageCode.Split('-')[0]));

            Biography = localizedText.Biography;
            Curriculum = localizedText.Curriculum;
        }
    }

    public class AuthorEditModel
    {
        public List<AuthorTextEditModel> Texts { get; set; }

        public AuthorEditModel()
        {
            Texts = new List<AuthorTextEditModel>
            {
                new AuthorTextEditModel
                {
                    LanguageCode = "pt",
                    Curriculum = "",
                    Biography = ""
                }
            };
        }
    }

    public class AuthorTextEditModel
    {
        public string LanguageCode { get; set; }
        public string Curriculum { get; set; }
        public string Biography { get; set; }
    }
}