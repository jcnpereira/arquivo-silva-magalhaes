using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.Models.SiteModels
{
    /// <summary>
    /// Database that supports the site.
    /// </summary>
    public class SiteDataContext : DbContext
    {
        public SiteDataContext() : base("name=ArchiveDataContext") { }
    }
}