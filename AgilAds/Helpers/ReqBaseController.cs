using AgilAds.Attributes;
using AgilAds.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AgilAds.Helpers
{
    public class ReqBaseController : Controller
    {
        private IUnitOfWorkAsync _uow;
        protected stackFrame.stackContext _currentContext;
        public ReqBaseController(IUnitOfWorkAsync uow, stackFrame.stackContext context)
        {
            _uow = uow;
            _currentContext = context;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           if (stackFrame.VerifyContext(this, _currentContext))
           {
               var bail = new RouteValueDictionary(
                   new { controller = "Home", action = "Index" });
               if (!Valid.FilterAccess(this, filterContext, _uow))
               {
                   filterContext.HttpContext.Response.RedirectToRoute(bail);
               }
           }
           else filterContext.HttpContext.Response.RedirectToRoute(stackFrame.Backup());
        }

        public ActionResult Back()
        {
            return RedirectToRoute(stackFrame.Backup());
        }
    }
}