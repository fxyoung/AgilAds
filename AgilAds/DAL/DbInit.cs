using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AgilAds.Models;
//using AgilAds.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AgilAds.DAL
{
    public class DbInit :
        System.Data.Entity.DropCreateDatabaseAlways<AgilAdsDataContext>
    {
        static ApplicationUserManager userManager;

        static ApplicationRoleManager roleManager;
        protected override void Seed(AgilAdsDataContext context)
        {
            base.Seed(context);
            InitializeIdentityForEF(context);
        }
        private void InitializeIdentityForEF(AgilAdsDataContext context)
        {
            const string superUser = "fxyoung";
            const string superUserEmail = "fxyoung@hotmail.com";
            const string password = "Firef0x.com";
            const string roleName = "SuperUser";

            userManager = HttpContext
            .Current.GetOwinContext()
            .GetUserManager<ApplicationUserManager>();

            roleManager = HttpContext.Current
            .GetOwinContext()
            .Get<ApplicationRoleManager>();

            //var roleStore = new RoleStore<ApplicationRole, int, ApplicationUserRole>(context);
            // roleManager = new RoleManager<ApplicationRole, int>(roleStore);
            //var userStore = new UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context);
            // userManager = new UserManager<ApplicationUser, int>(userStore);   

            //Create roles that do not yet exist
            var roles = Enum.GetNames(typeof(AgilAds.Helpers.Constants.roleMaster)).ToList();
            foreach (var role in roles)
            {
                if (roleManager.FindByName(role) == null)
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            var user = userManager.FindByName(superUser);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = superUser,
                    Email = superUserEmail,
                    Status = roleName,
                    RepId = -1
                };
                userManager.Create(user, password);
                userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user rolename if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(roleName))
            {
                var result = userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}