using ArquivoSilvaMagalhaes.Models.Translations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class ReferencedLink
    {
        public ReferencedLink()
        {
            Translations = new List<ReferencedLinkTranslation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(ResourceType = typeof(ReferencedLinkStrings), Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(ResourceType = typeof(ReferencedLinkStrings), Name = "Link")]
        public string Link { get; set; }

        public virtual IList<ReferencedLinkTranslation> Translations { get; set; }
    }

    public class ReferencedLinkTranslation : EntityTranslation
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public override string LanguageCode { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [Display(ResourceType = typeof(ReferencedLinkStrings), Name = "Description")]
        public string Description { get; set; }

        [ForeignKey("Id")]
        public virtual ReferencedLink Link { get; set; }
    }

}