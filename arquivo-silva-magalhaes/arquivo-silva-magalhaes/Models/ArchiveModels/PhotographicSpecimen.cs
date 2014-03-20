using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class PhotographicSpecimen
    {
        public PhotographicSpecimen()
        {
            this.KeyWords = new HashSet<KeyWord>();
            this.DigitalPhotos = new HashSet<DigitalPhoto>();
        }

        public int Id { get; set; }
        public string AuthorCode { get; set; }
        public string Notes { get; set; }
        public string InterventionDescription { get; set; }
        public string Topic { get; set; }
        public System.DateTime SpecimenDate { get; set; }

        public virtual PhotographicProcess PhotographicProcess { get; set; }
        public virtual PhotographicFormat PhotographicFormat { get; set; }
        public virtual Document Document { get; set; }

        /// <summary>
        /// Keywords associated with this specimen.
        /// </summary>
        public virtual ICollection<KeyWord> KeyWords { get; set; }

        /// <summary>
        /// Digital photos about this specimen.
        /// </summary>
        public virtual ICollection<DigitalPhoto> DigitalPhotos { get; set; }
    }
}