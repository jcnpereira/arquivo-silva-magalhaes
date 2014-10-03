using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Web.I18n;
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
}