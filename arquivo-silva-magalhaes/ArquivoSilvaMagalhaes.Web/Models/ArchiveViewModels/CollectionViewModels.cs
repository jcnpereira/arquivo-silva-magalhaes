using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public partial class CollectionEditViewModel
    {
        public Collection Collection { get; set; }

        [Display(ResourceType = typeof(ImageStrings), Name = "Authors")]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }

        //[ContentType(ContentType = "image/*", ErrorMessage = "A")]
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionLogo")]
        public HttpPostedFileBase Logo { get; set; }

        [Display(ResourceType = typeof(CollectionStrings), Name = "Authors")]
        [Required(ErrorMessageResourceType = typeof(ValidationErrorStrings), ErrorMessageResourceName = "MustChooseAtLeastOne")]
        public int[] AuthorIds { get; set; }
    }
}