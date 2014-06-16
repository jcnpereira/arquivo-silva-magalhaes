using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
            this.Translations = new List<CollectionTranslation>();
            this.Documents = new List<Document>();
            this.Authors = new List<Author>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of this collection.
        /// </summary>
        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// The type of this collection.
        /// </summary>
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionType"), Required]
        public CollectionType Type { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "InitialProductionDate")]
        public DateTime InitialProductionDate { get; set; }

        /// <summary>
        /// The date on which this collection was created.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "EndProductionDate")]
        public DateTime EndProductionDate { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        public string LogoLocation { get; set; }

        /// <summary>
        /// If the collection has attachments.
        /// </summary>
        [Display(ResourceType = typeof(DataStrings), Name = "HasAttachments")]
        public bool HasAttachments { get; set; }

        /// <summary>
        /// Location of the logo of this collection.
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "AttachmentsDescriptions")]
        public string AttachmentsDescriptions { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "OrganizationSystem")]
        public string OrganizationSystem { get; set; }

        
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public  bool IsVisible { get; set; }

        /// <summary>
        /// Code used by the archive to physically catalog this
        /// collection.
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogCode")]
        public  string CatalogCode { get; set; }

        public virtual List<CollectionTranslation> Translations { get; set; }
        public virtual List<Document> Documents { get; set; }
        public virtual List<Author> Authors { get; set; }

        [Display(Name = "Autores desta coleção")]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }

        [NotMapped]
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

        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

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