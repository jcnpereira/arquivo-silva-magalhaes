using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArquivoSilvaMagalhaes.Models.Translations;

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
        [StringLength(60)]
        [Display(ResourceType = typeof(AuthorStrings), Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name(s) of this author.
        /// </summary>
        [Required]
        [StringLength(60)]
        [Display(ResourceType = typeof(AuthorStrings), Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// The date on which this author was born.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(AuthorStrings), Name = "BirthDate")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The date on which this author died.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(AuthorStrings), Name = "DeathDate")]
        public DateTime? DeathDate { get; set; }

        [Display(ResourceType = typeof(AuthorStrings), Name = "PictureFileName")]
        public string PictureFileName { get; set; }

        /// <summary>
        /// Localized texts descibing the biography and other aspects of this
        /// author.
        /// </summary>
        public virtual IList<AuthorTranslation> Translations { get; set; }

        /// <summary>
        /// Documents created by this author.
        /// </summary>
        public virtual IList<Document> Documents { get; set; }

        /// <summary>
        /// Collections created by this author.
        /// </summary>
        public virtual IList<Collection> Collections { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DeathDate.HasValue)
            {
                if (DeathDate.Value.CompareTo(BirthDate) < 0)
                {
                    yield return new ValidationResult(AuthorStrings.ValidationError_Dates, new string[] { "BirthDate", "DeathDate" });
                }
            }
        }
    }

    /// <summary>
    /// Text details about this author.
    /// </summary>
    public partial class AuthorTranslation : EntityTranslation
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
        public override string LanguageCode { get; set; }

        /// <summary>
        /// The nationality of this author. eg. Portuguese.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(ResourceType = typeof(AuthorStrings), Name = "Nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// Text containing the biography of this author.
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(AuthorStrings), Name = "Biography")]
        public string Biography { get; set; }

        /// <summary>
        /// Text containing the curriculum of this author.
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(AuthorStrings), Name = "Curriculum")]
        public string Curriculum { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}