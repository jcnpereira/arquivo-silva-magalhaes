using ArquivoSilvaMagalhaes.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public partial class CollectionViewModel
    {
        [NotMapped]
        public int[] AuthorIds { get; set; }

        [Required, NotMapped]
        [Display(Name = "Autores desta coleção")]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }

        [NotMapped]
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionLogo")]
        public HttpPostedFileBase Logo { get; set; }
    }

    public partial class CollectionTranslation
    {
        [Required, NotMapped]
        [Display(ResourceType = typeof(DataStrings), Name = "CollectionLogo")]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}