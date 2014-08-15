using ArquivoSilvaMagalhaes.Resources;
using System;
using System.ComponentModel.DataAnnotations;

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
        public TechnicalDocument()
        {
            LastModificationDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(DataStrings), Name = "FileName")]
        public string FileName { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "UploadDate")]
        public DateTime UploadDate { get; set; }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(DataStrings), Name = "LastModificationDate")]
        public DateTime LastModificationDate { get; set; }

        public String Format { get; set; }

        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "FileSize")]
        public int FileSize { get; set; }
    }
}