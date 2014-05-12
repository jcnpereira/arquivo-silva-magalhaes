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
        }
    
        [Key]
        public int Id { get; set; }
        public string CatalogCode { get; set; }
        public string AuthorCatalogationCode { get; set; }
        public bool HasMarksOrStamps { get; set; }
        public string Indexation { get; set; }
        public string Notes { get; set; }

        [Required]
        public virtual Document Document { get; set; }

        public virtual Process Process { get; set; }
        public virtual Format Format { get; set; }

        public virtual Classification Classification { get; set; }
        public virtual ICollection<DigitalPhotograph> DigitalPhotographs { get; set; }
        public virtual ICollection<SpecimenText> SpecimenTexts { get; set; }

        #region Non-mapped attributes

        [NotMapped]
        public string Code
        {
            get
            {
                var collectionCode = Document.Collection.CatalogCode;
                var documentCode = Document.CatalogCode;

                return String.Format("{0}.{1}.{2}", collectionCode, documentCode, CatalogCode);
            }
        }

        #endregion
    }

    public partial class SpecimenText
    {
        public SpecimenText()
        {
            this.LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string SimpleStateDescription { get; set; }
        public string DetailedStateDescription { get; set; }
        public string InterventionDescription { get; set; }
        public string Publication { get; set; }
    
        [Required]
        public virtual Specimen Specimen { get; set; }
    }
}
