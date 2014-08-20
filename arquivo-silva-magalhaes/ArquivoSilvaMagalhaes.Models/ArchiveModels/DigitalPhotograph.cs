using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class DigitalPhotograph
    {
        public DigitalPhotograph()
        {
            ScanDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DigitalPhotographStrings), Name = "ScanDate")]
        public DateTime? ScanDate { get; set; }

        public string FileName { get; set; }
        public string MimeType { get; set; }


        [Display(ResourceType = typeof(DigitalPhotographStrings), Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        public int SpecimenId { get; set; }
        [ForeignKey("SpecimenId")]
        public virtual Specimen Specimen { get; set; }
    }
}