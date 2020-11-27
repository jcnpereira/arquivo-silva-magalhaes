﻿using ArquivoSilvaMagalhaes.Models.Translations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public enum DocumentType : byte
    {
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "DocumentType_Image")]
        Image = 1,
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "DocumentType_Text")]
        Text = 2,
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "DocumentType_Video")]
        Video = 3,
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "DocumentType_Other")]
        Other = 100
    }

    public class TechnicalDocument
    {
        public TechnicalDocument()
        {
            LastModificationDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [StringLength(100)]
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "FileName")]
        public string FileName { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "UploadDate")]
        public DateTime UploadDate { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "LastModificationDate")]
        public DateTime LastModificationDate { get; set; }

        public String Format { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(TechnicalDocumentStrings), Name = "FileSize")]
        public int FileSize { get; set; }
    }
}