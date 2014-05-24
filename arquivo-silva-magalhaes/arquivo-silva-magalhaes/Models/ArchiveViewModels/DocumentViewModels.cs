using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public partial class Document
    {
        [Required]
        [NotMapped]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }

        [Required]
        [NotMapped]
        public IEnumerable<SelectListItem> AvailableCollections { get; set; }
    }

    public partial class DocumentTranslation
    {
        [Required]
        [NotMapped]
        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}
