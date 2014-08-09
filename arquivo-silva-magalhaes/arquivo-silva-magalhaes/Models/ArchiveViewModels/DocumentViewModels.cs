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
        public IList<SelectListItem> AvailableAuthors { get; set; }
        [Display(ResourceType = typeof(DocumentStrings), Name = "Collection")]
        public IList<SelectListItem> AvailableCollections { get; set; }

        public IList<SelectListItem> AvailableLanguages { get; set; }
    }
}
