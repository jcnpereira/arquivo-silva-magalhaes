namespace ArquivoSilvaMagalhaes.Models
{
    using System;
    using System.Collections.Generic;
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

    public partial class Author
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
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of this author.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The place where this author was born.
        /// </summary>
        public string Nationality { get; set; }
        public System.DateTime BirthDate { get; set; }
        public Nullable<System.DateTime> DeathDate { get; set; }
        public string Biography { get; set; }
        public string Curriculum { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
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
        public virtual ICollection<KeyWord> KeyWords { get; set; }
        public virtual ICollection<DigitalPhoto> DigitalPhotos { get; set; }
    }
}