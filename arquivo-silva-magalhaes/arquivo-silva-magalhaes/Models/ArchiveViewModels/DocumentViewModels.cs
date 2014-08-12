using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public class DocumentEditViewModel
    {
        public Document Document { get; set; }

        
        public IList<SelectListItem> AvailableAuthors { get; set; }
        
        public IList<SelectListItem> AvailableCollections { get; set; }

        public IList<SelectListItem> AvailableLanguages { get; set; }
    }
}
