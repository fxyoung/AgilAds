using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Summary()
        {
            using (var db = new AgilAds.DAL.AgilAdsDataContext())
            {
                var AdInfoes = db.AdInfoes.Count();
                var Admins = db.Admins.Count();
                var BusinessInfoes = db.BusinessInfoes.Count();
                var Institutions = db.Institutions.Count();
                var Members = db.Members.Count();
                var People = db.People.Count();
                var Privs = db.Privs.Count();
                var Reps = db.Reps.Count();
            }
            return RedirectToAction("Index");
        }
    }
}