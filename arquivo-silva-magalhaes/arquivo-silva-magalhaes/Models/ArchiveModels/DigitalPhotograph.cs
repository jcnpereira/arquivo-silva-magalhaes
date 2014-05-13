using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class DigitalPhotograph
    {
        public DigitalPhotograph()
        {
            this.ShowcasePhotos = new HashSet<ShowcasePhoto>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime ScanDate { get; set; }

        public string StoreLocation { get; set; }

        public string Process { get; set; }

        public string CopyrightInfo { get; set; }

        public string IsVisible { get; set; }

        [Required]
        public int SpecimenId { get; set; }

        public virtual ICollection<ShowcasePhoto> ShowcasePhotos { get; set; }
    }
}