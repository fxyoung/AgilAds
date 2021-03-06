﻿using AgilAds.DAL;
using AgilAds.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Attributes
{
    public static class Valid
    {
        public enum Context
        {
            Representative, Admin,
            Member, Institution,
            People, BusinessInfo,
            Role, Priviledge,
            toggle
        }
        public enum Action { create, read, write, delete, list }
        public enum Users { Admin, All }

        public static bool FilterAccess(Controller controller,
            ActionExecutingContext filter, IUnitOfWorkAsync uow)
        {
            bool retval = true;
            bool throwOnNotAuthorized = false;
            bool ownerRole =
                Helpers.Startup.OwnerRoles.
                Any(a => controller.User.IsInRole(a));
            bool adminRole = false;
            if (!ownerRole)
            {
                adminRole =
                    Helpers.Startup.AdminRoles.
                    Any(a => controller.User.IsInRole(a));
            }
            var priv = getPrivileges(controller.User.Identity.Name, uow);
            RequirementAttribute[] att =
                (RequirementAttribute[])filter.ActionDescriptor.
                GetCustomAttributes(typeof(RequirementAttribute), true);
            if (att != null)
            {
                // if current user enjoys all of the privileges
                // required by the routine's Feature attributes
                // then the routine can be safely executed
                for (int j = 0; j < att.Length; j++)
                {
                    if (!throwOnNotAuthorized)
                        throwOnNotAuthorized = att[j]._throwIfNotAuthorized;
                    if (att[j]._users.Equals(Valid.Users.Admin))
                    {
                        if (!adminRole) continue;
                    }
                    if (!priv.ContainsKey(att[j]._context))
                    {
                        retval = false;
                        break;
                    }
                    if (!priv[att[j]._context].Contains(att[j]._action))
                    {
                        retval = false;
                        break;
                    }
                }
            }
            if (!retval && throwOnNotAuthorized)
                throw new UnauthorizedAccessException();
            return retval;
        }

        private static Dictionary<Valid.Context, List<Valid.Action>>
            getPrivileges(string username, IUnitOfWorkAsync uow)
        {
            var retval = new Dictionary<Valid.Context, List<Valid.Action>>();
            if (String.IsNullOrWhiteSpace(username)) return retval;
            var privs = new AgilAds.BusinessServices.Administration.PrivServicesAsync(uow).
                GetPrivsForUser(username);
            foreach (var priv in privs)
            {
                if (!retval.ContainsKey(priv.Context))
                    retval.Add(priv.Context, new List<Action>());
                if (!retval[priv.Context].Contains(priv.Action)) retval[priv.Context].Add(priv.Action);
            }
            return retval;
        }
    }
}