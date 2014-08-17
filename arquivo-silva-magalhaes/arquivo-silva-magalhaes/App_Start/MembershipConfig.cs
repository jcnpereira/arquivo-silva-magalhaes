using ArquivoSilvaMagalhaes.Models;
using ArquivoSilvaMagalhaes.Utilitites;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ArquivoSilvaMagalhaes.App_Start
{
    public class MembershipConfig
    {
        public static void SeedMembership()
        {
            using (var db = new ApplicationDbContext())
            {
                var userManager =
                        new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                var roleManager =
                    new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                var password = "kDBMFkp80ZbQ";

                var adminRole = new IdentityRole(MembershipUtils.AdminRoleName);
                var siteRole = new IdentityRole(MembershipUtils.PortalRoleName);
                var archiveRole = new IdentityRole(MembershipUtils.ArchiveRoleName);
                var contentRole = new IdentityRole(MembershipUtils.ContentRoleName);

                var userName = "Admin";

                var user = new ApplicationUser
                {
                    UserName = userName
                };

                var userCreateResult = userManager.Create(user, password);
                roleManager.Create(adminRole);
                roleManager.Create(siteRole);
                roleManager.Create(archiveRole);
                roleManager.Create(contentRole);

                if (!userCreateResult.Succeeded)
                {
                    user = userManager.FindByName(userName);
                }

                userManager.AddToRole(user.Id, adminRole.Name);
            }
        }
    }
}