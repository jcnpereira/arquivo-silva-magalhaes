using Microsoft.AspNet.Identity.EntityFramework;

namespace ArquivoSilvaMagalhaes.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<ArquivoSilvaMagalhaes.Models.SiteModels.TechnicalDocument> TechnicalDocuments { get; set; }

        public System.Data.Entity.DbSet<ArquivoSilvaMagalhaes.Models.SiteModels.Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<ArquivoSilvaMagalhaes.Areas.BackOffice.ViewModels.NewsItemViewModels> NewsItemViewModels { get; set; }
    }
}