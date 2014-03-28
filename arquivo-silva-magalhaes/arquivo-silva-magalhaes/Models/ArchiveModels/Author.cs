using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Author : IValidatableObject
    {
        public Author()
        {
            this.AuthorTexts = new HashSet<AuthorText>();
            this.Documents = new HashSet<Document>();
            this.Collections = new HashSet<Collection>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }

        public virtual ICollection<AuthorText> AuthorTexts { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }

    public partial class AuthorText
    {
        public AuthorText()
        {
            this.LanguageCode = "pt";
        }

        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }
        public string Curriculum { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}