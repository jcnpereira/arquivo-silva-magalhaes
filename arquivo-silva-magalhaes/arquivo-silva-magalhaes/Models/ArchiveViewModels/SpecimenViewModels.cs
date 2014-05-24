using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public partial class Specimen
    {
        [NotMapped]
        public IEnumerable<SelectListItem> AvailableDocuments { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> AvailableProcesses { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> AvailableFormats { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> AvailableClassfications { get; set; }

        [Required, NotMapped]
        public int[] KeywordIds { get; set; }
        public IEnumerable<SelectListItem> AvailableKeywords { get; set; }
    }


    // TODO: Implement this!
    public class SpecimenTranslation
    {
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }

    public class SpecimenPhotoUploadModel
    {

        public int Id { get; set; }

        [Required]
        public int SpecimenId { get; set; }

        public List<PhotoUploadModelItem> Items { get; set; }

        public List<HttpPostedFileBase> Photos { get; set; }
    }

    public class PhotoUploadModelItem
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ScanDate { get; set; }

        [Required]
        public string Process { get; set; }

        public string OriginalFileName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string CopyrightInfo { get; set; }

        public bool IsVisible { get; set; }

        public bool IsToConsider { get; set; }
    }
}