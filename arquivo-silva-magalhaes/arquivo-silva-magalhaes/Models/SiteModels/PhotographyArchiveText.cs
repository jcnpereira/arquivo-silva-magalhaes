using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class PhotographyArchiveText
    {

        public PhotographyArchiveText(){
            CodPhotograohicArchive = new HashSet<PhotographicArchive>();
        }
       

        public string CodLanguage { get; set;}
        public string CodText {get; set;}
        public string ArchiveHistory { get; set; }
        public string ArchiveMission { get; set; }

       public virtual ICollection<PhotographicArchive> CodPhotograohicArchive { get; set; }

        
    }
}