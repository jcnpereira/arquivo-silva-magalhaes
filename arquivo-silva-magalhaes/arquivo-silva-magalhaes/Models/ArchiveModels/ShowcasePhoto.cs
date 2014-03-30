using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class ShowcasePhoto
    {
        public ShowcasePhoto()
        {
            this.ShowcasePhotoTexts = new HashSet<ShowcasePhotoText>();
        }

        [Key]
        public int Id { get; set; }
        public string CommenterName { get; set; }
        public string CommenterEmail { get; set; }
        public string IsEmailVisible { get; set; }
        public string VisibleSince { get; set; }
        public int DigitalPhotographId { get; set; }

        [Required]
        public virtual DigitalPhotograph DigitalPhotograph { get; set; }
        public virtual ICollection<ShowcasePhotoText> ShowcasePhotoTexts { get; set; }
    }

    public partial class ShowcasePhotoText
    {
        public ShowcasePhotoText()
        {
            this.LanguageCode = "pt";
        }

        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string LanguageCode { get; set; }
        public string Comment { get; set; }
        public int ShowcasePhotoId { get; set; }

        [Required]
        public virtual ShowcasePhoto ShowcasePhoto { get; set; }
    }
}