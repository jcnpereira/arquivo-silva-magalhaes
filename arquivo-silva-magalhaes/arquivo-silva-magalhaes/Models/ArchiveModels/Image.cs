using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class Image
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Subject { get; set; }

        public DateTime ProductionDate { get; set; }

        public string Publication { get; set; }
        public string Location { get; set; }



    }
}