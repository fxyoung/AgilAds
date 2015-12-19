using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilAds.Helpers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using AgilAds.Models;
    using AgilAds.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity.EntityFramework;
    using AgilAds.DAL;
    public static class Constants
    {
        // rolename constants 
        //public enum roleMaster { SuperUser, SA, Representative, RA, Institution, Member }
        //public enum OwnerRoles { SuperUser, Representative }
        //public enum AdministrativeRoles { SA, RA }

        // Registration screen default values
        public const string defaultRole = "Dormant";
        public const int defaultRefId = -1;
        public const int orgNameMax = 80;
        public const int orgNameMin = 2;
        public const string orgNameRegexPattern = "^[a-zA-Z][a-zA-Z0-9.@$!#%?_' ]*";
        public const int bigBuffer = 2000;
        public const int userNameMax = 25;
        public const int userNameMin = 6;
        public const string userNameRegexPattern = "^[a-zA-Z][a-zA-Z0-9.@$!#%?_]*";
        public const int regionNameMax = 25;
        public const int regionNameMin = 2;
        public const string regionNameRegexPattern = "^[a-zA-Z][a-zA-Z0-9 ]*";
        public const int indivNameMax = 25;
        public const int indivNameMin = 2;
        public const string indivNameRegexPattern = "^[a-zA-Z][a-zA-Z]*";
    }

    public static class Startup
    {
        private static ApplicationUserManager _userManager = null;
        private static ApplicationUserManager userManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
                }
                return _userManager;
            }
        }
        public static List<string> rolesMaster { get; private set; }
        public static List<string> OwnerRoles { get; private set; }
        public static List<string> AdminRoles { get; private set; }
        public static string defaultUser { get; private set; }
        private enum startupVar
        { Username, UserRole, UserEmail, UserPassword, Rolenames, OwnerRoles, AdminRoles }
        private static Dictionary<startupVar, string> GetStartupSettings()
        {
            var retval = new Dictionary<startupVar, string>();
            var prefix = "Startup:";
            var webConfig = System.Web.Configuration.WebConfigurationManager.AppSettings;//["configFile"];//.OpenWebConfiguration("configFile");
            if (webConfig.Count > 0)
            {
                foreach (var startupVar in Enum.GetNames(typeof(startupVar)))
                {
                    var varname = prefix + startupVar;
                    var setting = webConfig[varname];
                    if(setting != null)
                    {
                        startupVar key = (startupVar)Enum.Parse(typeof(startupVar), startupVar);
                        if(!retval.ContainsKey(key))
                        {
                            retval.Add(key,setting);
                        }
                    }
                }
            }
            return retval;
        }
        public static void getConfigDefaults()
        {
            var settings = GetStartupSettings();
            rolesMaster = settings[startupVar.Rolenames]
                .Split(new char[] { '|' }).ToList();
            OwnerRoles = settings[startupVar.OwnerRoles]
                .Split(new char[] { '|' }).ToList();
            AdminRoles = settings[startupVar.AdminRoles]
                .Split(new char[] { '|' }).ToList();
            defaultUser = settings[startupVar.Username];
        }
        public static void ConfigDefaultUser(ApplicationDbContext context)
        {
            var settings = GetStartupSettings();
            string superUser = settings[startupVar.Username];
            string superUserEmail = settings[startupVar.UserEmail];
            string password = settings[startupVar.UserPassword];
            string roleName = settings[startupVar.UserRole];

            // userManager = HttpContext
            //.Current.GetOwinContext()
            //.GetUserManager<ApplicationUserManager>();

            //Create roles that do not yet exist
            var reqRoles = settings[startupVar.Rolenames]
                .Split(new char[] { '|' });
            var currRoles = context.Roles.ToList();
            foreach (var role in reqRoles)
            {
                if (!currRoles.Any(r => r.Name.Equals(role)))
                {
                    context.Roles.Add(new IdentityRole(role));
                }
            }
            context.SaveChanges();
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
            defaultUser = superUser;
        }
        public static void AddUserRole(string username, string roleName, bool throwIfUserNotFound = false)
        {
            // Add user rolename if not already added
            var user = userManager.FindByName(username);
            if (user != null)
            {
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(roleName))
                {
                    var result = userManager.AddToRole(user.Id, roleName);
                }
            }
            else if (throwIfUserNotFound) throw new ArgumentException("User " + username + " not defined");
        }
    }

    public static class Utils
    {
        public static string FindNameOf(int id)
        {
            var name = "not defined";
            using (var db = new AgilAdsDataContext())
            {
                var fp = db.People.Find(id);
                if (fp != null) name = fp.Fullname;
            }
            return name;
        }
        public static List<System.Web.Mvc.SelectListItem> MakeSelectList(string[] e,
            string selectedItem = null)
        {
            var l = new List<System.Web.Mvc.SelectListItem>();
            var s = e.ToList();
            s.Sort();
            foreach (var str in s)
            {
                l.Add(new System.Web.Mvc.SelectListItem()
                {
                    Text = str,
                    Value = str,
                    Selected = str.Equals(selectedItem)
                });
            }
            return l;
        }
    }
}