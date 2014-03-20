namespace ArquivoSilvaMagalhaes.Models
{
    using ArquivoSilvaMagalhaes.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class ArchiveDataContext : DbContext
    {
        public ArchiveDataContext() : base("name=ArchiveDataContext") { }

        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<PhotographicSpecimen> PhotographicSpecies { get; set; }
        public virtual DbSet<PhotographicProcess> PhotographicProcesses { get; set; }
        public virtual DbSet<PhotographicFormat> PhotographicFormats { get; set; }
        public virtual DbSet<KeyWord> KeyWords { get; set; }
        public virtual DbSet<DigitalPhoto> DigitalPhotos { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
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

    public partial class Collection
    {
        public Collection()
        {
            this.Dimension = 0;
        }

        public int Id { get; set; }
        public string Provenience { get; set; }
        public short Dimension { get; set; }
        public string HistoricalDetails { get; set; }
        public string Type { get; set; }
        public System.DateTime ProductionDate { get; set; }
    }

    public partial class DigitalPhoto
    {
        public int Id { get; set; }

        public virtual PhotographicSpecimen PhotographicSpecimen { get; set; }
    }

    public partial class Document
    {
        public Document()
        {
            this.PhotographicSpecimens = new HashSet<PhotographicSpecimen>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime DocumentDate { get; set; }
        public System.DateTime CatalogDate { get; set; }
        public string Notes { get; set; }
        public int CollectionId { get; set; }
        public int AuthorId { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<PhotographicSpecimen> PhotographicSpecimens { get; set; }
    }

    public partial class KeyWord
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }

    public partial class PhotographicFormat
    {
        public int Id { get; set; }
        public string FormatDescription { get; set; }
    }

    public partial class PhotographicProcess
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
    }

    public partial class PhotographicSpecimen
    {
        public PhotographicSpecimen()
        {
            this.KeyWords = new HashSet<KeyWord>();
            this.DigitalPhotos = new HashSet<DigitalPhoto>();
        }

        public int Id { get; set; }
        public string AuthorCode { get; set; }
        public string Notes { get; set; }
        public string InterventionDescription { get; set; }
        public string Topic { get; set; }
        public System.DateTime SpecimenDate { get; set; }

        public virtual PhotographicProcess PhotographicProcess { get; set; }
        public virtual PhotographicFormat PhotographicFormat { get; set; }
        public virtual Document Document { get; set; }

        /// <summary>
        /// Keywords associated with this specimen.
        /// </summary>
        public virtual ICollection<KeyWord> KeyWords { get; set; }

        /// <summary>
        /// Digital photos about this specimen.
        /// </summary>
        public virtual ICollection<DigitalPhoto> DigitalPhotos { get; set; }
    }
}