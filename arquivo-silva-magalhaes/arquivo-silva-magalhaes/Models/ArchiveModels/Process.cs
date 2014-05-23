

using ArquivoSilvaMagalhaes.Resources;
using System.ComponentModel.DataAnnotations;
namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public enum LanguageCode : byte
    {
        [Display()]
        pt = 1,
        en = 2,
        Other = 100
    }
    public class Process
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(ResourceType = typeof(DataStrings), Name = "LanguageCode")]
        public LanguageCode LanguageCode { get; set; }
        
        








        /*   public Process()
           {
               this.ProcessTexts = new HashSet<ProcessText>();
               this.Specimens = new HashSet<Specimen>();
           }

           [Key]
           public int Id { get; set; }

           public virtual ICollection<ProcessText> ProcessTexts { get; set; }
           public virtual ICollection<Specimen> Specimens { get; set; }
       }

       public partial class ProcessText
       {
           public ProcessText()
           {
               this.LanguageCode = "pt";
           }

           [Key, Column(Order = 0)]
           public int Id { get; set; }

           public string LanguageCode { get; set; }
           public string Value { get; set; }
           public int ProcessId { get; set; }

           [Required]
           public virtual Process Process { get; set; }*/
    }
}