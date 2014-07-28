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
            var roleName = "admins";
            var userName = "Admin";

            var user = new ApplicationUser
            {
                UserName = userName
            };

            var role = new IdentityRole
            {
                Name = roleName
            };

            var userCreateResult = userManager.Create(user, password);
            var roleCreateResult = roleManager.Create(role);

            if (!userCreateResult.Succeeded)
            {
                user = userManager.FindByName(userName);
            }

            userManager.AddToRole(user.Id, roleName);
        }
    }
}