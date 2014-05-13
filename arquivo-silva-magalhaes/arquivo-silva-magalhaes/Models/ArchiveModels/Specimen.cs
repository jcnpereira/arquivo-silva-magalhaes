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
            this.SpecimenTexts = new HashSet<SpecimenText>();
            this.DigitalPhotographs = new HashSet<DigitalPhotograph>();
            this.Classifications = new HashSet<Classification>();
        }
    
        [Key]
        public int Id { get; set; }
        public string CatalogCode { get; set; }
        public string AuthorCatalogationCode { get; set; }
        public bool HasMarksOrStamps { get; set; }
        public string Indexation { get; set; }
        public string Notes { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        public int ProcessId { get; set; }

        [Required]
        public int FormatId { get; set; }

        public virtual ICollection<Classification> Classifications { get; set; }
        public virtual ICollection<DigitalPhotograph> DigitalPhotographs { get; set; }
        public virtual ICollection<SpecimenText> SpecimenTexts { get; set; }
        public virtual ICollection<Keyword> Keywords { get; set; }

        
    }

    public partial class SpecimenText
    {
        [Key, Column(Order = 0)]
        public int SpecimenId { get; set; }

        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string SimpleStateDescription { get; set; }
        public string DetailedStateDescription { get; set; }
        public string InterventionDescription { get; set; }
        public string Publication { get; set; }
    }
}
