using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class DigitalPhotograph
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }
        public string MimeType { get; set; }

        [Required]
        public int SpecimenId { get; set; }
        [ForeignKey("SpecimenId")]
        public virtual Specimen Specimen { get; set; }
    }
}