using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Image
    {
        public Image()
        {
            Keywords = new HashSet<Keyword>();
            Specimens = new HashSet<Specimen>();
            Translations = new HashSet<ImageTranslation>();
        }

        public int Id { get; set; }

        [Display(ResourceType = typeof (DataStrings), Name = "Image__ProductionDate")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string ImageCode { get; set; }

        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        public int DocumentId { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }

        public int? DigitalPhotographId { get; set; }
        [ForeignKey("DigitalPhotographId")]
        public virtual DigitalPhotograph DigitalPhotograph { get; set; }

        public virtual ICollection<ImageTranslation> Translations { get; set; }
    }

    public class ImageTranslation
    {
        [Key, Column(Order = 0)]
        public int ImageId { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Subject { get; set; }

        public string Publication { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}