using ArquivoSilvaMagalhaes.Models.ArchiveModels;
using ArquivoSilvaMagalhaes.Resources;
using ArquivoSilvaMagalhaes.Resources.ModelTranslations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ArquivoSilvaMagalhaes.Models.ArchiveViewModels
{
    public class DocumentEditViewModel
    {
        public Document Document { get; set; }

        [Display(ResourceType = typeof(DocumentStrings), Name = "Author")]
        public IEnumerable<SelectListItem> AvailableAuthors { get; set; }
        [Display(ResourceType = typeof(DocumentStrings), Name = "Collection")]
        public IEnumerable<SelectListItem> AvailableCollections { get; set; }

        public IEnumerable<SelectListItem> AvailableLanguages { get; set; }
    }
}
