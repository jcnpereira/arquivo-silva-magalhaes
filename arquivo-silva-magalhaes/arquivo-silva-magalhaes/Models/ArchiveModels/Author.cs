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
            this.Documents = new HashSet<Document>();
            this.Collections = new HashSet<Collection>();
        }

        public int Id { get; set; }

        /// <summary>
        /// The first name of this author.
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "AuthorFirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of this author.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// The place where this author was born.
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// The date on which this author was born.
        /// </summary>
        public System.DateTime BirthDate { get; set; }

        /// <summary>
        /// The date on which this author died.
        /// </summary>
        public System.DateTime DeathDate { get; set; }

        /// <summary>
        /// Text containing the biography of this author.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// Text containing the author's curriculum.
        /// </summary>
        public string Curriculum { get; set; }

        /// <summary>
        /// Documents associated with this author.
        /// </summary>
        public virtual ICollection<Document> Documents { get; set; }

        /// <summary>
        /// Collections associated with this author.
        /// </summary>
        public virtual ICollection<Collection> Collections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // The death date has to be later than the birth date.
            if (DeathDate.CompareTo(BirthDate) < 0)
            {
                yield return new ValidationResult(ErrorStrings.DeathDateEarlierThanBirthDate);
            }
        }
    }
}