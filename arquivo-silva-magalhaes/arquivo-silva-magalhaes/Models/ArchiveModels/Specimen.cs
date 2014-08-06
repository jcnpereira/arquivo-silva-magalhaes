using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public enum SpecimenState : byte
    {
        [Display(ResourceType = typeof(SpecimenStrings), Name = "State_1")]
        Poor = 1,
        [Display(ResourceType = typeof(SpecimenStrings), Name = "State_2")]
        Mediocre = 2,
        [Display(ResourceType = typeof(SpecimenStrings), Name = "State_3")]
        Fair = 3,
        [Display(ResourceType = typeof(SpecimenStrings), Name = "State_4")]
        Good = 4,
        [Display(ResourceType = typeof(SpecimenStrings), Name = "State_5")]
        VeryGood = 5
    }

    public class Specimen
    {
        public Specimen()
        {
            this.Translations = new List<SpecimenTranslation>();
            this.DigitalPhotographs = new List<DigitalPhotograph>();

            State = SpecimenState.Fair;
        }
    
        [Key]
        public int Id { get; set; }

        [Display(ResourceType = typeof(SpecimenStrings), Name = "AuthorCatalogationCode")]
        public string AuthorCatalogationCode { get; set; }

        [Display(ResourceType = typeof(SpecimenStrings), Name = "HasMarksOrStamps")]
        public bool HasMarksOrStamps { get; set; }

        [Display(ResourceType = typeof(SpecimenStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "State")]
        public SpecimenState? State { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "ReferenceCode")]
        public string ReferenceCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Image")]
        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        [Required]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Process")]
        public int ProcessId { get; set; }
        [ForeignKey("ProcessId")]
        public virtual Process Process { get; set; }

        [Required]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Format")]
        public int FormatId { get; set; }
        [ForeignKey("FormatId")]
        public virtual Format Format { get; set; }

        public virtual IList<DigitalPhotograph> DigitalPhotographs { get; set; }

        public virtual IList<SpecimenTranslation> Translations { get; set; }
    }

    public class SpecimenTranslation
    {
        [Key, Column(Order = 0)]
        public int SpecimenId { get; set; }
        [ForeignKey("SpecimenId")]
        public Specimen Specimen { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Description")]
        public string Description { get; set; }

        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "DetailedStateDescription")]
        public string DetailedStateDescription { get; set; }

        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "InterventionDescription")]
        public string InterventionDescription { get; set; }
    }
}
