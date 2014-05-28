using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [Display(ResourceType = typeof(DataStrings), Name = "Description")]
        public string FormatDescription { get; set; }

        public virtual ICollection<Specimen> Specimens { get; set; }
    }
}