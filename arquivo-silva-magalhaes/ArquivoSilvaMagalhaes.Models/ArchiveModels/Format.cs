using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Format
    {
        public Format()
        {
            this.Specimens = new HashSet<Specimen>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        [Display(ResourceType = typeof(FormatStrings), Name = "Format")]
        public string FormatDescription { get; set; }

        public virtual ICollection<Specimen> Specimens { get; set; }
    }
}