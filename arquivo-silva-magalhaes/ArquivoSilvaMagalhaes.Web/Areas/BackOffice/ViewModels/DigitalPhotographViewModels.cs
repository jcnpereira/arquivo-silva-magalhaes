using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class DigitalPhotographPostUploadModel
    {
        public DigitalPhotographPostUploadModel()
        {
            UploadedItems = new List<DigitalPhotographUploadItem>();
        }

        public List<DigitalPhotographUploadItem> UploadedItems { get; set; }
    }

    public class DigitalPhotographUploadItem
    {
        public DigitalPhotographUploadItem()
        {
            Save = true;
        }

        public DigitalPhotograph DigitalPhotograph { get; set; }
        [Display(ResourceType = typeof(DigitalPhotographStrings), Name = "Save")]
        public bool Save { get; set; }
    }
}