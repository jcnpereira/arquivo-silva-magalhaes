﻿using ArquivoSilvaMagalhaes.Models.Translations;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels {
   public partial class Document : IValidatableObject {
      public Document() {
         this.Translations = new List<DocumentTranslation>();
         this.Images = new List<Image>();
      }

      [Key]
      public int Id { get; set; }

      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [StringLength(100)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "Title")]
      public string Title { get; set; }

      /// <summary>
      /// The name of the person that is responsible for this collection.
      /// </summary>
      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [StringLength(80)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "ResponsibleName")]
      public string ResponsibleName { get; set; }

      /// <summary>
      /// The date on which this document was made.
      /// </summary>
      //[DataType(DataType.Date)]
      //[Display(ResourceType = typeof(DocumentStrings), Name = "DocumentDate")]
      //public DateTime? DocumentDate { get; set; }

      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [Display(ResourceType = typeof(DocumentStrings), Name = "DocumentDate")]
      public string DocumentDate { get; set; }

      /// <summary>
      /// The date on which this document was catalogued.
      /// </summary>
      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [DataType(DataType.Date)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "CatalogationDate")]
      public DateTime CatalogationDate { get; set; }

      /// <summary>
      /// Notes issued by the people in the archive about
      /// this document.
      /// </summary>
      [DataType(DataType.MultilineText)]
      [StringLength(1000)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "Notes")]
      public string Notes { get; set; }

      /// <summary>
      /// Code used by the people in the archive to
      /// physically catalog this document.
      /// </summary>
      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [StringLength(200)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "CatalogCode")]
      [Index(IsUnique = true)]
      [RegularExpression("[0-9A-Za-z/_:.;-]+", ErrorMessageResourceType = typeof(DocumentStrings), ErrorMessageResourceName = "CodeFormat")]
      public string CatalogCode { get; set; }

      [Display(ResourceType = typeof(DocumentStrings), Name = "Collection")]
      public int CollectionId { get; set; }

      [ForeignKey("CollectionId")]
      [Display(ResourceType = typeof(DocumentStrings), Name = "Collection")]
      public virtual Collection Collection { get; set; }

      [Display(ResourceType = typeof(DocumentStrings), Name = "Author")]
      public int AuthorId { get; set; }

      [Display(ResourceType = typeof(DocumentStrings), Name = "Author")]
      [ForeignKey("AuthorId")]
      public virtual Author Author { get; set; }

      public virtual IList<DocumentTranslation> Translations { get; set; }

      public virtual IList<Image> Images { get; set; }

      public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
         if (CatalogationDate > DateTime.Now) {
            yield return new ValidationResult(DocumentStrings.Validation_CataloguedInTheFuture, new string[] { "CatalogationDate" });
         }
      }
   }

   public partial class DocumentTranslation : EntityTranslation {
      [Key, Column(Order = 0)]
      public int DocumentId { get; set; }

      [Key, Column(Order = 1), Required]
      public override string LanguageCode { get; set; }

      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [StringLength(100)]
      [DataType(DataType.MultilineText)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "DocumentLocation")]
      public string DocumentLocation { get; set; }

      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [StringLength(500)]
      [DataType(DataType.MultilineText)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "FieldAndContents")]
      public string FieldAndContents { get; set; }

      [Required(ErrorMessage = "O/A {0} é de preenchimento obrigatório.")]
      [StringLength(500)]
      [DataType(DataType.MultilineText)]
      [Display(ResourceType = typeof(DocumentStrings), Name = "Description")]
      public string Description { get; set; }

      [ForeignKey("DocumentId")]
      public virtual Document Document { get; set; }
   }
}