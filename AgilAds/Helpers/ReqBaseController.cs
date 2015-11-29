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
        private IUnitOfWork _uow;
        public ReqBaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var bail = new RouteValueDictionary(
                new { controller = "Home", action = "Index" });
            if (!Valid.FilterAccess(this, filterContext, _uow))
            {
                filterContext.HttpContext.Response.RedirectToRoute(bail);
            }
        }
    }
}