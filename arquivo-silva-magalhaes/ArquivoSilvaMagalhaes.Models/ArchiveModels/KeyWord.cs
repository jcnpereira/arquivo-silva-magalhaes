using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class Keyword
    {
        public Keyword()
        {
            this.Translations = new List<KeywordTranslation>();
            this.Documents = new List<Document>();
            this.Images = new List<Image>();
        }

        [Key]
        public int Id { get; set; }

        public virtual IList<KeywordTranslation> Translations { get; set; }
        public virtual IList<Document> Documents { get; set; }
        public virtual IList<Image> Images { get; set; }
    }

    public partial class KeywordTranslation
    {
        [Key, Column(Order = 0)]
        public int KeywordId { get; set; }

        [Key, Column(Order = 1), Required]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(50), Index(IsUnique = true)]
        [Display(ResourceType = typeof(KeywordStrings), Name = "Value")]
        public string Value { get; set; }

        [ForeignKey("KeywordId")]
        public virtual Keyword Keyword { get; set; }
    }
}