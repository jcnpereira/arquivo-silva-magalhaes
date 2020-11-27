﻿using ArquivoSilvaMagalhaes.Models.SiteModels;
using ArquivoSilvaMagalhaes.Models.Translations;
using ArquivoSilvaMagalhaes.Web.I18n;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{
    public class TechnicalDocumentEditViewModel
    {
        public TechnicalDocument TechnicalDocument { get; set; }

        [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "UploadedFile")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}