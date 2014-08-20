using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        public string MimeFormat { get; set; }

        [Required]
        [Display(ResourceType = typeof(AttachmentStrings), Name = "FileName")]
        public string FileName { get; set; }

        [Required]
        [Display(ResourceType = typeof(AttachmentStrings), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(AttachmentStrings), Name = "Size")]
        public int Size { get; set; }
    }
}