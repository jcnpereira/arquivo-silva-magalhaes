using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public class DocumentEditViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// The name of the person that is responsible for this collection.
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Display(ResourceType = typeof(DataStrings), Name = "ResponsibleName")]
        public string ResponsibleName { get; set; }

        /// <summary>
        /// The date on which this document was made.
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentDate")]
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// The date on which this document was catalogued.
        /// </summary>
        /// [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogationDate")]
        public DateTime CatalogationDate { get; set; }

        /// <summary>
        /// Notes issued by the people in the archive about
        /// this document.
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        [Display(ResourceType = typeof(DataStrings), Name = "Notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Code used by the people in the archive to
        /// physically catalog this document.
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogCode")]
        public string CatalogCode { get; set; }

        [Required]
        public int CollectionId { get; set; }
        [Required]
        public IEnumerable<SelectListItem> AvailableCollections { get; set; }

        [Required]
        public int AuthorId { get; set; }
        [Required]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }


        
        [Required]
        public int[] KeywordIds { get; set; }
        [Required]
        public IEnumerable<SelectListItem> AvailableKeywords { get; set; }

        public List<DocumentTranslationEditViewModel> Translations { get; set; }
    }

    public partial class DocumentTranslationEditViewModel
    {
        public int DocumentId { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentLocation")]
        public string DocumentLocation { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "FieldAndContents")]
        public string FieldAndContents { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}
