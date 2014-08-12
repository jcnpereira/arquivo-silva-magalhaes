using ArquivoSilvaMagalhaes.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class SpotlightVideo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "UriPathVideo")]
        public string UriPath { get; set;}
        [Required]
        [DataType(DataType.Date),DisplayFormat( DataFormatString="{0:dd/MM/yy}", ApplyFormatInEditMode=true )]
        [Display(ResourceType = typeof(DataStrings), Name = "PublicationDate")]
        public DateTime PublicationDate { get; set; }
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [Display(ResourceType = typeof(DataStrings), Name = "RemotionDate")]
        public DateTime RemotionDate { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "IsPermanent")]
        public bool IsPermanent { get; set; }
    }
}