using Microsoft.AspNet.Identity.EntityFramework;

namespace ArquivoSilvaMagalhaes.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string RealName { get; set; }
        public string EmailAddress { get; set; }
        public string PictureUrl { get; set; }
    }

    class ApplicationConfig
    {
        public int MaxImageWidth { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

    }
}