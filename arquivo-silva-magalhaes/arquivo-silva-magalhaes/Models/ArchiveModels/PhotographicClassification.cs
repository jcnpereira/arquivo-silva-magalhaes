using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.ArchiveModels
{
    public class PhotographicClassification
    {
        [DefaultValue(typeof(DataStrings), "NoClassification")]
        public string Classification { get; set; }

        public virtual ICollection<PhotographicSpecimen> PhotographicSpecimens { get; set; }
    }
}