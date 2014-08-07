using ArquivoSilvaMagalhaes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquivoSilvaMagalhaes.App_Start
{
    public class MembershipConfig
    {
        public static void SeedMembership()
        {
            var db = new ApplicationDbContext();

            var userManager =
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var roleManager =
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var password = "kDBMFkp80ZbQ";

            var adminRole = new IdentityRole("admins");
            var siteRole = new IdentityRole("sitemanagers");
            var archiveRole = new IdentityRole("archivemanagers");

            var userName = "Admin";

            var user = new ApplicationUser
            {
                UserName = userName
            };

            var userCreateResult = userManager.Create(user, password);
            roleManager.Create(adminRole);
            roleManager.Create(siteRole);
            roleManager.Create(archiveRole);

            if (!userCreateResult.Succeeded)
            {
                user = userManager.FindByName(userName);
            }

            userManager.AddToRole(user.Id, adminRole.Name);
        }
    }
}