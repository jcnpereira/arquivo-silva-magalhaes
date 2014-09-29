using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{
    public class EventEditViewModel
    {
        public Event Event { get; set; }

        [Display(ResourceType = typeof(EventStrings), Name = "Attachments")]
        public IList<Attachment> Attachments { get; set; }
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}
