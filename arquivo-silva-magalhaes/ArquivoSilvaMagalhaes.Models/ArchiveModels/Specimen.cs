using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public class Specimen// : IValidatableObject
    {
        public Specimen()
        {
            this.Translations = new List<SpecimenTranslation>();
            this.DigitalPhotographs = new List<DigitalPhotograph>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "AuthorCatalogationCode")]
        public string AuthorCatalogationCode { get; set; }

        [Display(ResourceType = typeof(SpecimenStrings), Name = "HasMarksOrStamps")]
        public bool HasMarksOrStamps { get; set; }

        [StringLength(300)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "State")]
        public SpecimenState? State { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Index(IsUnique = true)]
        [StringLength(100)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "ReferenceCode")]
        [RegularExpression("^[A-Za-z0-9]+-[A-Za-z0-9]+-[A-Za-z0-9]+-[A-Za-z0-9]+$", ErrorMessageResourceType = typeof(SpecimenStrings), ErrorMessageResourceName = "CodeFormat")]
        public string ReferenceCode { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(50)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Annotation")]
        [RegularExpression("[0-9A-Za-z/_:.;-]+", ErrorMessageResourceType = typeof(SpecimenStrings), ErrorMessageResourceName = "ArchivalCodeFormat")]
        public string Annotation { get; set; }
        
        #region Navigation Properties
        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Image")]
        public int ImageId { get; set; }
        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Process")]
        public int ProcessId { get; set; }
        [ForeignKey("ProcessId")]
        public virtual Process Process { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Format")]
        public int FormatId { get; set; }
        [ForeignKey("FormatId")]
        public virtual Format Format { get; set; }

        public virtual IList<DigitalPhotograph> DigitalPhotographs { get; set; }

        public virtual IList<SpecimenTranslation> Translations { get; set; } 
        #endregion
    }

    public class SpecimenTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int SpecimenId { get; set; }
        [ForeignKey("SpecimenId")]
        public Specimen Specimen { get; set; }

        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(300)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Description")]
        public string Description { get; set; }

        [StringLength(200)]
        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "DetailedStateDescription")]
        public string DetailedStateDescription { get; set; }

        [StringLength(200)]
        [Required, DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "InterventionDescription")]
        public string InterventionDescription { get; set; }
    }
}
