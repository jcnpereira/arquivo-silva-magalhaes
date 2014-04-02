using ArquivoSilvaMagalhaes.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    public class LanguageDefinitions
    {
        public static readonly IList<string> AcceptedLanguages = new ReadOnlyCollection<string>
        (new List<string>{
            "pt",
            "en"
        });
    }
}