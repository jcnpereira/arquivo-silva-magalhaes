using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    /// <summary>
    /// Defines an author of a collection, or an author of a document.
    /// </summary>
    public partial class Author : IValidatableObject
    {
        public Author()
        {
            this.AuthorTexts = new HashSet<AuthorText>();
            this.Documents = new HashSet<Document>();
            this.Collections = new HashSet<Collection>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The first name(s) of this author.
        /// </summary>
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
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// The date on which this author died.
        /// </summary>
        [Required]
        public DateTime DeathDate { get; set; }

        /// <summary>
        /// Localized texts descibing the biography and other aspects of this
        /// author.
        /// </summary>
        public virtual ICollection<AuthorText> AuthorTexts { get; set; }
        /// <summary>
        /// Documents created by this author.
        /// </summary>
        public virtual ICollection<Document> Documents { get; set; }
        /// <summary>
        /// Collections created by this author.
        /// </summary>
        public virtual ICollection<Collection> Collections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // The death date cannot be earlier than the birth date.
            if (DeathDate.CompareTo(BirthDate) < 0)
            {
                yield return new ValidationResult(ErrorStrings.DeathDateEarlierThanBirthDate);
            }

        }

        #region Non-mapped attributes
        [NotMapped]
        public string Biography
        {
            get
            {
                var authText = this.AuthorTexts
                    .First(text => Thread.CurrentThread.CurrentUICulture.ToString().Contains(text.LanguageCode) || 
                        text.LanguageCode.Contains(AuthorText.DefaultLanguageCode));

                return authText.Biography;
            }
        }

        [NotMapped]
        public string Curriculum
        {
            get
            {
                var authText = this.AuthorTexts
                    .First(text => Thread.CurrentThread.CurrentUICulture.ToString().Contains(text.LanguageCode) || 
                        text.LanguageCode.Contains(AuthorText.DefaultLanguageCode));

                return authText.Curriculum;
            }
        }
        #endregion
    }

    /// <summary>
    /// Text details about this author.
    /// </summary>
    public partial class AuthorText
    {
        [NotMapped]
        public const string DefaultLanguageCode = "pt";

        public AuthorText()
        {
            this.LanguageCode = DefaultLanguageCode;
        }

        /// <summary>
        /// The author which is associated with this detail text.
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string LanguageCode { get; set; }

        /// <summary>
        /// The nationality of this author. eg. Portuguese.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Nationality { get; set; }

        /// <summary>
        /// Text containing the biography of this author.
        /// </summary>
        [Required]
        public string Biography { get; set; }

        /// <summary>
        /// Text containing the curriculum of this author.
        /// </summary>
        [Required]
        public string Curriculum { get; set; }

        [Required]
        public virtual Author Author { get; set; }
        
    }
}