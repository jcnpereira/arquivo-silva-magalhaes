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
    
    public partial class Process
    {
        public Process()
        {
            this.ProcessTexts = new HashSet<ProcessText>();
            this.Specimens = new HashSet<Specimen>();
        }
    
        public int Id { get; set; }
    
        public virtual ICollection<ProcessText> ProcessTexts { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }
}