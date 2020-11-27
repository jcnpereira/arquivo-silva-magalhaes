using ArquivoSilvaMagalhaes.Models.Translations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        public string MimeFormat { get; set; }

       [Required(ErrorMessage ="O/A {0} é de preenchimento obrigatório.")]
        [Display(ResourceType = typeof(AttachmentStrings), Name = "FileName")]
        public string FileName { get; set; }

        [StringLength(50)]
        [Display(ResourceType = typeof(AttachmentStrings), Name = "Title")]
        public string Title { get; set; }

        [Display(ResourceType = typeof(AttachmentStrings), Name = "Size")]
        public int Size { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public int EventId { get; set; }
    }
}