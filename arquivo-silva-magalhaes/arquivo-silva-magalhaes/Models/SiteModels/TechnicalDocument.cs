using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public enum DocumentType : byte
    {
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentType_Image")]
        Image = 1,
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentType_Text")]
        Text = 2,
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentType_Video")]
        Video = 3,
        [Display(ResourceType = typeof(DataStrings), Name = "DocumentType_Other")]
        Other = 100
    }

    public class TechnicalDocument
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "FileName")]
        public string FileName { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "UploadDate")]
        public DateTime UploadDate { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LastModificationDate")]
        public DateTime LastModificationDate { get; set; }

        [Required]
        public String Format { get; set; }


        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "TechnicalDocument_DocumentType")]
        public DocumentType DocumentType { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Language")]
        public string Language { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "FileSize")]
        public int FileSize { get; set; }
    }
}