using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Specimen
    {
        public Specimen()
        {
            this.SpecimenTexts = new HashSet<SpecimenText>();
            this.Classification = new HashSet<Classification>();
            this.DigitalPhotographs = new HashSet<DigitalPhotograph>();
        }
    
        public int Id { get; set; }
        public string CatalogCode { get; set; }
        public string AuthorCatalogationCode { get; set; }
        public bool HasMarksOrStamps { get; set; }
        public string Indexation { get; set; }
        public string Notes { get; set; }
        public int FormatId { get; set; }
        public int DocumentId { get; set; }
    
        public virtual ICollection<SpecimenText> SpecimenTexts { get; set; }
        public virtual Format Format { get; set; }
        public virtual Document Document { get; set; }
        public virtual Process Process { get; set; }
        public virtual ICollection<Classification> Classification { get; set; }
        public virtual ICollection<DigitalPhotograph> DigitalPhotographs { get; set; }
    }

    public partial class SpecimenText
    {
        public SpecimenText()
        {
            this.LanguageCode = "pt";
        }
    
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string SimpleStateDescription { get; set; }
        public string DetailedStateDescription { get; set; }
        public string InterventionDescription { get; set; }
        public string Publication { get; set; }
        public int SpecimenId { get; set; }
    
        public virtual Specimen Specimen { get; set; }
    }
}
