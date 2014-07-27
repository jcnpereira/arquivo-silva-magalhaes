using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{


    public class ImageViewModel
    {
        public Image Image { get; set; }

        public IEnumerable<SelectListItem> AvailableKeywords { get; set; }
        public IEnumerable<SelectListItem> AvailableDocuments { get; set; }
    }

    public class ImageEditViewModel
    {
        public Image Image { get; set; }

        [Required]
        public IEnumerable<SelectListItem> AvailableKeywords { get; set; }
        [Required]
        public IEnumerable<SelectListItem> AvailableDocuments { get; set; }
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