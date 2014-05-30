using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Specimen
    {
        public Specimen()
        {
            this.Translations = new HashSet<SpecimenTranslation>();
            this.DigitalPhotographs = new HashSet<DigitalPhotograph>();
            this.Classifications = new HashSet<Classification>();
            this.Keywords = new HashSet<Keyword>();
        }
    
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "CatalogCode")]
        public string CatalogCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "AuthorCatalogCode")]
        public string AuthorCatalogationCode { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "HasMarksOrStamps")]
        public bool HasMarksOrStamps { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Indexation")]
        public string Indexation { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        public int DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }

        [Required]
        public int ProcessId { get; set; }
        [ForeignKey("ProcessId")]
        public virtual Process Process { get; set; }

        [Required]
        public int FormatId { get; set; }
        [ForeignKey("FormatId")]
        public virtual Format Format { get; set; }

        
        public virtual ICollection<Classification> Classifications { get; set; }
        [NotMapped]
        public int[] ClassificationIds { get; set; }

        public virtual ICollection<DigitalPhotograph> DigitalPhotographs { get; set; }

        public virtual ICollection<SpecimenTranslation> Translations { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }
        [NotMapped]
        public int[] KeywordIds { get; set; }
    }

    public partial class SpecimenTranslation
    {
        [Key, Column(Order = 0)]
        public int SpecimenId { get; set; }
        [ForeignKey("SpecimenId")]
        public Specimen Specimen { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Topic")]
        public string Topic { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "StateSimple")]
        public string SimpleStateDescription { get; set; }

        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "StateDetailed")]
        public string DetailedStateDescription { get; set; }

        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "InterventionDescription")]
        public string InterventionDescription { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Publication")]
        public string Publication { get; set; }
    }
}
