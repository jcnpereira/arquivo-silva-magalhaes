using ArquivoSilvaMagalhaes.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.SiteViewModels
{
    public class SpotLightVideoModels
    {

        [Key]
        [Required]
        public int Id { get; set; }
        
        [Display(ResourceType = typeof(DataStrings), Name = "UriPathVideo")]
        public string UriPath { get; set;}
        //[Required]
        //[Display(ResourceType = typeof(DataStrings), Name = "PublicationDate")]
        //[DataType(DataType.Date)]
        //public DateTime PublicationDate { get; set; }
        //[Required]
        //[Display(ResourceType = typeof(DataStrings), Name = "RemotionDate")]
        //[DataType(DataType.Date)]
        //public DateTime RemotionDate { get; set; }
        //[Required]
        //[Display(ResourceType = typeof(DataStrings), Name = "IsPermanent")]
        //public bool IsPermanent { get; set; }
    
    }

}