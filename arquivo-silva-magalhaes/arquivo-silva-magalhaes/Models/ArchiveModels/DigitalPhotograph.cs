using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class DigitalPhotograph
    {
        public DigitalPhotograph()
        {
            this.ShowcasePhotoes = new HashSet<ShowcasePhoto>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int SpecimenId { get; set; }

        public DateTime ScanDate { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public string Process { get; set; }

        public string CopyrightInfo { get; set; }

        public bool IsVisible { get; set; }

        public string Encoding { get; set; }

        [ForeignKey("SpecimenId")]
        public Specimen Specimen { get; set; }

        public ICollection<ShowcasePhoto> ShowcasePhotoes { get; set; }
    }
}