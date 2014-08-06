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
    public partial class DigitalPhotograph
    {
        public DigitalPhotograph()
        {
            LastModified = DateTime.Now;
            ShowcasePhotoes = new List<ShowcasePhoto>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int SpecimenId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DigitalPhotographStrings), Name = "ScanDate")]
        public DateTime ScanDate { get; set; }

        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string MimeType { get; set; }
        public DateTime LastModified { get; set; }

        [Required]
        [Display(ResourceType = typeof(DigitalPhotographStrings), Name = "ScanProcess")]
        public string Process { get; set; }

        [Required]
        [Display(ResourceType = typeof(DigitalPhotographStrings), Name = "CopyrightInfo")]
        public string CopyrightInfo { get; set; }

        [Display(ResourceType = typeof(DigitalPhotographStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        [ForeignKey("SpecimenId")]
        public virtual Specimen Specimen { get; set; }

        public virtual IList<ShowcasePhoto> ShowcasePhotoes { get; set; }
    }
}