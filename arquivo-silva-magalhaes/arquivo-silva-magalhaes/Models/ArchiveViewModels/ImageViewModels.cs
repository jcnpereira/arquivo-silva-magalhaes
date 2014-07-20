using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{


    public class ImageViewModel
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Image__ProductionDate")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string ImageCode { get; set; }

        [ForeignKey("DocumentId")]
        public Document Document { get; set; }
        public int DocumentId { get; set; }

        public virtual IEnumerable<Keyword> Keywords { get; set; }
        public virtual IEnumerable<Specimen> Specimens { get; set; }

        public int? DigitalPhotographId { get; set; }
        [ForeignKey("DigitalPhotographId")]
        public DigitalPhotograph DigitalPhotograph { get; set; }

        public virtual IEnumerable<ImageTranslation> Translations { get; set; }

        public string Title { get; set; }
        public string Location { get; set; }
    }

    public class ImageCreateModel
    {
        [Display(ResourceType = typeof(DataStrings), Name = "Image__ProductionDate")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string ImageCode { get; set; }


    }
}