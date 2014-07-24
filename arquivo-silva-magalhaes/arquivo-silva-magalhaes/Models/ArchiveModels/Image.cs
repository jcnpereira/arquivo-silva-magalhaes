using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Image : TranslateableEntity<ImageTranslation>
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
        public virtual Document Document { get; set; }
        public int DocumentId { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }

        public int? DigitalPhotographId { get; set; }
        [ForeignKey("DigitalPhotographId")]
        public virtual DigitalPhotograph DigitalPhotograph { get; set; }

        // public new ICollection<ImageTranslation> Translations { get; set; }

        [Required]
        [NotMapped]
        public string Title
        {
            get
            {
                return GetTranslatedValueOrDefault("Title");
            }

            set
            {
                SetTranslatedValue("Title", value);
            }
        }

        //public virtual ICollection<ImageTranslation> Translations { get; set; }
    }

    public class ImageTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int ImageId { get; set; }
        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [Required]
        public string Title { get; set; }

        //[Required]
        public string Subject { get; set; }
        
        public string Publication { get; set; }
        //[Required]
        public string Location { get; set; }
        //[Required]
        public string Description { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }
    }
}