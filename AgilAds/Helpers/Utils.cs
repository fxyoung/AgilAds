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