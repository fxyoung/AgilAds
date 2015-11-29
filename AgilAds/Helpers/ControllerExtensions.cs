using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Helpers
{
    public static class ControllerExtensions
    {
        public static bool InOperableState(this Controller controller)
        {
            var manager = HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            var appUser = manager.FindByName(controller.User.Identity.Name);
            return controller.User.IsInRole(appUser.Status);
        }

        public static int GetRepId(this Controller controller)
        {
            var manager = HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            var appUser = manager.FindByName(controller.User.Identity.Name);
            return appUser.RepId;
        }
    }
}