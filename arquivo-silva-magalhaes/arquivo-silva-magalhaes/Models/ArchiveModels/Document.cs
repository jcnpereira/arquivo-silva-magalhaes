﻿using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Document
    {
        public Document()
        {
            this.Translations = new List<DocumentTranslation>();
            this.Images = new List<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DocumentStrings), Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// The name of the person that is responsible for this collection.
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "ResponsibleName")]
        public string ResponsibleName { get; set; }

        /// <summary>
        /// The date on which this document was made.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "DocumentDate")]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// The date on which this document was catalogued.
        /// </summary>
        /// [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "CatalogationDate")]
        public DateTime CatalogationDate { get; set; }

        /// <summary>
        /// Notes issued by the people in the archive about
        /// this document.
        /// </summary>
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "Notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Code used by the people in the archive to
        /// physically catalog this document.
        /// </summary>
        [Required]
        [MaxLength(200), Index(IsUnique = true)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "CatalogCode")]
        public string CatalogCode { get; set; }

        public int CollectionId { get; set; }

        [ForeignKey("CollectionId")]
        public virtual Collection Collection { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

        public virtual IList<DocumentTranslation> Translations { get; set; }

        public virtual IList<Image> Images { get; set; }
    }

    public partial class DocumentTranslation
    {
        [Key, Column(Order = 0)]
        public int DocumentId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "DocumentLocation")]
        public string DocumentLocation { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "FieldAndContents")]
        public string FieldAndContents { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DocumentStrings), Name = "Description")]
        public string Description { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }
    }
}