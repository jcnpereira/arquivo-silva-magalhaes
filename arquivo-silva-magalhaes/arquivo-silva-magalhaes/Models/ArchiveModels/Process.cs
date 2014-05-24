using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Process
    {
        public Process()
        {
            this.Translations = new HashSet<ProcessTranslation>();
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        public virtual ICollection<ProcessTranslation> Translations { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }

    public partial class ProcessTranslation
    {
        [Key, Column(Order = 0)]
        public int ProcessId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Process")]
        public string Value { get; set; }

        [ForeignKey("ProcessId")]
        public Process Process { get; set; }
    }
}