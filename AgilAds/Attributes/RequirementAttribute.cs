using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilAds.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RequirementAttribute : Attribute
    {
        public Valid.Context _context { get; private set; }
        public Valid.Action _action { get; private set; }
        public Valid.Users _users { get; private set; }
        public bool _throwIfNotAuthorized { get; set; }
        public RequirementAttribute(
            Valid.Context context,
            Valid.Action action,
            Valid.Users users = Valid.Users.Admin,
            bool throwIfNotAuthorized = false)
        {
            _context = context;
            _action = action;
            _users = users;
            _throwIfNotAuthorized = throwIfNotAuthorized;
        }
    }
}