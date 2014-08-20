//using ArquivoSilvaMagalhaes.Models.Translations;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace ArquivoSilvaMagalhaes.Models.SiteModels
//{
//    public class Collaborator
//    {
//        public Collaborator()
//        {
//            EventsAsCollaborator = new HashSet<Event>();
//        }

//        [Key]
//        public int Id { get; set; }
//        [Required]
//        [Display(ResourceType = typeof(DataStrings), Name = "Name")]
//        public string Name { get; set; }
//        [Required]
//        [Display(ResourceType = typeof(DataStrings), Name = "EmailAddress")]
//        public string EmailAddress { get; set; }
//        [Required]
//        [Display(ResourceType = typeof(DataStrings), Name = "Task")]
//        public string Task { get; set; }
//        [Required]
//        [Display(ResourceType = typeof(DataStrings), Name = "ContactVisible")]
//        public bool ContactVisible { get; set; }
//        [Required]
//        [Display(ResourceType = typeof(DataStrings), Name = "Contact")]
//        public string Contact { get; set; }

//        public ICollection<Event> EventsAsCollaborator { get; set; }
//    }
//}