using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Web.I18n;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
    public partial class CollectionEditViewModel
    {
        public Collection Collection { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Authors")]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }

        //[ContentType(ContentType = "image/*", ErrorMessage = "A")]
        [Display(ResourceType = typeof(CollectionStrings), Name = "LogoLocation")]
        public HttpPostedFileBase Logo { get; set; }

        [Display(ResourceType = typeof(CollectionStrings), Name = "Authors")]
        [Required(ErrorMessageResourceType = typeof(LayoutStrings), ErrorMessageResourceName = "MustChooseAtLeastOne")]
        public int[] AuthorIds { get; set; }
    }
}