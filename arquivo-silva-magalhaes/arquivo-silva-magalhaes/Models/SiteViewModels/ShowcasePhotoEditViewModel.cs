using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.SiteViewModels
{
    public class ShowcasePhotoEditViewModel
    {
        public ShowcasePhoto ShowcasePhoto { get; set; }

        public IEnumerable<SelectListItem> AvailableImages { get; set; }
    }
}