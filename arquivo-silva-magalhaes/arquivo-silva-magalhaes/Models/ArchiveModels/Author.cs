using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    /// <summary>
    /// Defines an author of a collection, or an author of a document.
    /// </summary>
    public partial class Author : IValidatableObject
    {
        public Author()
        {
            this.Translations = new List<AuthorTranslation>();
            this.Documents = new List<Document>();
            this.Collections = new List<Collection>();
        }

        [Key]
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
        [Required]
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
        public virtual List<AuthorTranslation> Translations { get; set; }

        /// <summary>
        /// Documents created by this author.
        /// </summary>
        public virtual List<Document> Documents { get; set; }

        /// <summary>
        /// Collections created by this author.
        /// </summary>
        public virtual List<Collection> Collections { get; set; }

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
    /// Text details about this author.
    /// </summary>
    public partial class AuthorTranslation
    {
        /// <summary>
        /// The author which is associated with this detail text.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int AuthorId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public string LanguageCode { get; set; }

        /// <summary>
        /// The nationality of this author. eg. Portuguese.
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(ResourceType = typeof(DataStrings), Name = "Nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// Text containing the biography of this author.
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Biography")]
        public string Biography { get; set; }

        /// <summary>
        /// Text containing the curriculum of this author.
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Curriculum")]
        public string Curriculum { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}