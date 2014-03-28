using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class PhotographicArchive
    {
        public PhotographicArchive()
        {
            ArchiveText = new HashSet<PhotographyArchiveText>();
            Contacts = new HashSet<ArchiveContact>();
        }

        public string CodPhotographicArchive { get; set; }
        public  ICollection<PhotographyArchiveText> ArchiveText;
        public  ICollection<ArchiveContact> Contacts;
    }
}