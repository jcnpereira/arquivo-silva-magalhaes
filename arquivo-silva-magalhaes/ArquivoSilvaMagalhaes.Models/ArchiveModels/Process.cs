using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Process
    {
        public Process()
        {
            this.Translations = new List<ProcessTranslation>();
            this.Specimens = new List<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        public virtual IList<ProcessTranslation> Translations { get; set; }
        public virtual IList<Specimen> Specimens { get; set; }
    }

    public class ProcessTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int ProcessId { get; set; }

        [Key, Column(Order = 1), Required]
        public override string LanguageCode { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(100)]
        [Display(ResourceType = typeof(ProcessStrings), Name = "Value")]
        public string Value { get; set; }

        [ForeignKey("ProcessId")]
        public virtual Process Process { get; set; }
    }
}