using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AgilAds.Models;
//using AgilAds.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using AgilAds.Helpers;

namespace AgilAds.DAL
{
    public class DbInit :
        System.Data.Entity.DropCreateDatabaseAlways<AgilAdsDataContext>
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static ApplicationUserManager userManager;

        protected override void Seed(AgilAdsDataContext context)
        {
            base.Seed(context);
            InitializeIdentityForEF(context);
        }
        private void InitializeIdentityForEF(AgilAdsDataContext context)
        {
            var settings = StartupSettings.GetStartupSettings();
            string superUser = settings[StartupSettings.startupVar.Username];
            string superUserEmail = settings[StartupSettings.startupVar.UserEmail];
            string password = settings[StartupSettings.startupVar.UserPassword];
            string roleName = settings[StartupSettings.startupVar.UserRole];

            userManager = HttpContext
            .Current.GetOwinContext()
            .GetUserManager<ApplicationUserManager>();

            //var roleStore = new RoleStore<ApplicationRole, int, ApplicationUserRole>(context);
            // roleManager = new RoleManager<ApplicationRole, int>(roleStore);
            //var userStore = new UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context);
            // userManager = new UserManager<ApplicationUser, int>(userStore);   

            //Create roles that do not yet exist
//            var reqRoles = Enum.GetNames(typeof(AgilAds.Helpers.Constants.roleMaster)).ToList();
            var reqRoles = settings[StartupSettings.startupVar.Rolenames]
                .Split(new char[]{'|'});
            var currRoles = db.Roles.ToList();
            foreach (var role in reqRoles)
            {
                if (!currRoles.Any(r=>r.Name.Equals(role)))
                {
                    db.Roles.Add(new IdentityRole(role));
                }
            }
            db.SaveChanges();

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