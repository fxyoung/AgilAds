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
    public static class Constants
    {
        // rolename constants 
        public enum roleMaster { SuperUser, SA, Representative, RA, Institution, Member }
        public enum OwnerRoles { SuperUser, Representative }
        public enum AdministrativeRoles { SA, RA }

        // Registration screen default values
        public const string defaultRole = "Dormant";
        public const int defaultRefId = -1;
    }

    public static class StartupSettings
    {
        public enum startupVar
        { Username, UserRole, UserEmail, UserPassword, Rolenames, OwnerRoles, AdminRoles }
        public static Dictionary<startupVar, string> GetStartupSettings()
        {
            var retval = new Dictionary<startupVar, string>();
            var prefix = "Startup:";
            var webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            if (webConfig.AppSettings.Settings.Count > 0)
            {
                foreach (var startupVar in Enum.GetNames(typeof(startupVar)))
                {
                    var varname = prefix + startupVar;
                    var setting = webConfig.AppSettings.Settings[varname];
                    if(setting != null)
                    {
                        startupVar key = (startupVar)Enum.Parse(typeof(startupVar), startupVar);
                        if(!retval.ContainsKey(key))
                        {
                            retval.Add(key,setting.Value);
                        }
                    }
                }
            }
            return retval;
        }
    }

    public static class Utils
    {
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