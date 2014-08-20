using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
    public class SpecimenEditViewModel
    {
        public SpecimenEditViewModel()
        {
            AvailableImages = new List<SelectListItem>();

            AvailableProcesses = new List<SelectListItem>();
            AvailableFormats = new List<SelectListItem>();
        }

        public Specimen Specimen { get; set; }

        public List<SelectListItem> AvailableImages { get; set; }

        public List<SelectListItem> AvailableProcesses { get; set; }

        public List<SelectListItem> AvailableFormats { get; set; }
    }

    public class SpecimenPhotoUploadModel
    {
        [Required]
        public int SpecimenId { get; set; }

        [Required]
        [Display(ResourceType = typeof(SpecimenStrings), Name = "Photos")]
        public List<HttpPostedFileBase> Photos { get; set; }
    }

    public class PhotoUploadModelItem
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "ScanDate")]
        public DateTime ScanDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "ScanProcess")]
        public string Process { get; set; }


        public string OriginalFileName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(DataStrings), Name = "CopyrightInfo")]
        public string CopyrightInfo { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "IsVisible")]
        public bool IsVisible { get; set; }

        public bool IsToConsider { get; set; }
    }
}