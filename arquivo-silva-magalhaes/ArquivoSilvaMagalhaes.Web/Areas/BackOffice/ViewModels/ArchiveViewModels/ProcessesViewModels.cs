﻿using ArquivoSilvaMagalhaes.Web.I18n;
using System.ComponentModel.DataAnnotations;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.ArchiveViewModels
{
    public class ProcessViewModel 
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "Process")]
        public string Value { get; set; }
    }

    public partial class ProcessEditViewModel
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }

        public string LanguageCode { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Process")]
        public string Value { get; set; }
    }
}