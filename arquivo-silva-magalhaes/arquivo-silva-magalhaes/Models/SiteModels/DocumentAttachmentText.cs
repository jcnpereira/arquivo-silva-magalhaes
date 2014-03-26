using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    public class DocumentAttachmentText
    {


        public string CodLanguage { get; set; }
       
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DocumentAttachment> DocumentsUsingAttachmentText { get; set; }
    }
}