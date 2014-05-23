using ArquivoSilvaMagalhaes.Resources;
using System.ComponentModel.DataAnnotations;
namespace ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels
{
    public class ProcessesEditViewModel 
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
        public string Name { get; set; }

      
        [Required]
        [MaxLength(30)]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public string LanguageCode { get; set; }
        
    }
}