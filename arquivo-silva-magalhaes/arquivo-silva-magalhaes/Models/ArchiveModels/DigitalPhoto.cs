using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public partial class DigitalPhoto
    {
        public int Id { get; set; }

        public virtual PhotographicSpecimen PhotographicSpecimen { get; set; }
    }
}