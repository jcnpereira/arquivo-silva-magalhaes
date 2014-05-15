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
    /// Defines a collection.
    /// </summary>
    public partial class Collection
    {
        public Collection()
        {
            this.CollectionTexts = new HashSet<CollectionText>();
            this.Documents = new HashSet<Document>();
            this.Authors = new HashSet<Author>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The type of this collection.
        /// </summary>
        public CollectionType Type { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        public string LogoLocation { get; set; }

        public bool HasAttachments { get; set; }

        public string OrganizationCode { get; set; }

        public string Notes { get; set; }
        public bool IsVisible { get; set; }

        /// <summary>
        /// Code used by the archive to physically catalog this
        /// collection.
        /// </summary>
        public string CatalogCode { get; set; }

        public ICollection<CollectionText> CollectionTexts { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Author> Authors { get; set; }
    }

    public enum CollectionType : byte
    {
        Collection = 1,
        Fond = 2,
        Other = 100
    }

    public partial class CollectionText
    {
        [Key]
        [Column(Order = 0)]
        public int CollectionId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string LanguageCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Provenience { get; set; }

        public string AdministrativeAndBiographicStory { get; set; }

        public string Dimension { get; set; }

        public string FieldAndContents { get; set; }

        public string CopyrightInfo { get; set; }

        [ForeignKey("CollectionId")]
        public Collection Collection { get; set; }
    }
}