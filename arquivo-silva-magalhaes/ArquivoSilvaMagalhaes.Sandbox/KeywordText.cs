//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArquivoSilvaMagalhaes.Sandbox
{
    using System;
    using System.Collections.Generic;
    
    public partial class KeywordText
    {
        public KeywordText()
        {
            this.LanguageCode = "pt";
        }
    
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Value { get; set; }
        public int KeywordId { get; set; }
    
        public virtual Keyword Keyword { get; set; }
    }
}
