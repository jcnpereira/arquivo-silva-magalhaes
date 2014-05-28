using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public enum CollectionType : byte
    {
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionType_Collection")]
        Collection = 1,
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionType_Fond")]
        Fond = 2,
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionType_Other")]
        Other = 255
    }

    /// <summary>
    /// Defines a collection.
    /// </summary>
    public partial class Collection
    {
        public Collection()
        {
            this.Translations = new HashSet<CollectionTranslation>();
            this.Documents = new HashSet<Document>();
            this.Authors = new HashSet<Author>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The type of this collection.
        /// </summary>
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionType"), Required]
        public CollectionType Type { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        [Required, DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "ProductionDate")]
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        public string LogoLocation { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "HasAttachments")]
        public bool HasAttachments { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "OrganizationSystem")]
        public string OrganizationSystem { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Code used by the archive to physically catalog this
        /// collection.
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogCode")]
        public string CatalogCode { get; set; }

        public virtual ICollection<CollectionTranslation> Translations { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Author> Authors { get; set; }

        [NotMapped, Required]
        public int[] AuthorIds { get; set; }
    }

    public partial class CollectionTranslation
    {
        [Key]
        [Column(Order = 0)]
        public int CollectionId { get; set; }

        [Key]
        [Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Provenience")]
        public string Provenience { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "AdministrativeAndBiographicStory")]
        public string AdministrativeAndBiographicStory { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Dimension")]
        public string Dimension { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "FieldAndContents")]
        public string FieldAndContents { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CopyrightInfo")]
        public string CopyrightInfo { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; }
    }
}