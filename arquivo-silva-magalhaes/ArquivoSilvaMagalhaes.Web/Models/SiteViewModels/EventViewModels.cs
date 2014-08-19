using ArquivoSilvaMagalhaes.Models.SiteModels;
using System.ComponentModel.DataAnnotations;

namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public enum EventTypeViewModels : byte
    {
        //[Description("Exposição")]
        [Display()]
        Exposição = 1,
        Escolar = 2,
        Outro = 100
    }

    public class EventEditViewModel
    {
        public Event Event { get; set; }
    }

    public class EventI18nPartialModels
    {
      /*  public EventI18nPartialModels()
        {
            LanguageCode = "pt";
        }*/
        [Key]
        public int Id { get; set; }
        [Required]
        public string LanguageCode { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Heading { get; set; }
        [Required]
        public string SpotLight { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }
        
       // [ForeignKey("EventId")]
       // public virtual Event Event { get; set; }

    }
}
