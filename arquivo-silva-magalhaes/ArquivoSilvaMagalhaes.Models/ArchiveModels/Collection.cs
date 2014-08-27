using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public enum CollectionType : byte
    {
        [Display(ResourceType = typeof(CollectionStrings), Name = "Type_Collection")]
        Collection = 1,
        [Display(ResourceType = typeof(CollectionStrings), Name = "Type_Fond")]
        Fond = 2,
        [Display(ResourceType = typeof(CollectionStrings), Name = "Type_Other")]
        Other = 255
    }

    /// <summary>
    /// Defines a collection.
    /// </summary>
    public class Collection
    {
        public Collection()
        {
            this.Translations = new List<CollectionTranslation>();
            this.Documents = new List<Document>();
            this.Authors = new List<Author>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The type of this collection.
        /// </summary>
        [Display(ResourceType = typeof(CollectionStrings), Name = "Type"), Required]
        public CollectionType? Type { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "InitialProductionDate")]
        public DateTime InitialProductionDate { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "EndProductionDate")]
        public DateTime? EndProductionDate { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        [Display(ResourceType = typeof(CollectionStrings), Name = "LogoLocation")]
        [DataType(DataType.ImageUrl)]
        public string LogoLocation { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "AttachmentDescription")]
        public string AttachmentsDescriptions { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "OrganizationSystem")]
        public string OrganizationSystem { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Display(ResourceType = typeof(CollectionStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Code used by the archive to physically catalog this
        /// collection.
        /// </summary>
        [Required, MaxLength(100), Index(IsUnique = true)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "CatalogCode")]
        [RegularExpression("^[0-9A-Za-z]+$", ErrorMessageResourceType = typeof(CollectionStrings), ErrorMessageResourceName = "AlphanumericCode")]
        public string CatalogCode { get; set; }

        [Display(ResourceType = typeof(CollectionStrings), Name = "Translations")]
        public virtual IList<CollectionTranslation> Translations { get; set; }
        [Display(ResourceType = typeof(CollectionStrings), Name = "Documents")]
        public virtual IList<Document> Documents { get; set; }
        [Display(ResourceType = typeof(CollectionStrings), Name = "Authors")]
        public virtual IList<Author> Authors { get; set; }
    }

    public class CollectionTranslation : EntityTranslation
    {
        [Key]
        [Column(Order = 0)]
        public int CollectionId { get; set; }

        [Key]
        [Column(Order = 1), Required]
        public override string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(CollectionStrings), Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "Provenience")]
        public string Provenience { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(CollectionStrings), Name = "AdministrativeAndBiographicStory")]
        public string AdministrativeAndBiographicStory { get; set; }

        [Required]
        [Display(ResourceType = typeof(CollectionStrings), Name = "Dimension")]
        public string Dimension { get; set; }

        [Required]
        [Display(ResourceType = typeof(CollectionStrings), Name = "FieldAndContents")]
        public string FieldAndContents { get; set; }

        [Required]
        [Display(ResourceType = typeof(CollectionStrings), Name = "CopyrightInfo")]
        public string CopyrightInfo { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; }
    }
}