using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquivoSilvaMagalhaes.Models
{
    public abstract class EntityTranslation
    {
        public virtual string LanguageCode { get; set; }
    }
}
