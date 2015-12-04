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
using AgilAds.Attributes;

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
            var testdata = new adsys(context);
            //var town = testdata.addBusiness(new BusinessInfo());
            //var eslID = testdata.addRep("East St. Louis", new Person(), new BusinessInfo());
        }
        private void InitializeIdentityForEF(AgilAdsDataContext context)
        {
//            var settings = StartupSettings.GetStartupSettings();
//            string superUser = settings[StartupSettings.startupVar.Username];
//            string superUserEmail = settings[StartupSettings.startupVar.UserEmail];
//            string password = settings[StartupSettings.startupVar.UserPassword];
//            string roleName = settings[StartupSettings.startupVar.UserRole];

//            userManager = HttpContext
//            .Current.GetOwinContext()
//            .GetUserManager<ApplicationUserManager>();

//            //var roleStore = new RoleStore<ApplicationRole, int, ApplicationUserRole>(context);
//            // roleManager = new RoleManager<ApplicationRole, int>(roleStore);
//            //var userStore = new UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context);
//            // userManager = new UserManager<ApplicationUser, int>(userStore);   

//            //Create roles that do not yet exist
////            var reqRoles = Enum.GetNames(typeof(AgilAds.Helpers.Constants.roleMaster)).ToList();
//            var reqRoles = settings[StartupSettings.startupVar.Rolenames]
//                .Split(new char[]{'|'});
//            var currRoles = db.Roles.ToList();
//            foreach (var role in reqRoles)
//            {
//                if (!currRoles.Any(r=>r.Name.Equals(role)))
//                {
//                    db.Roles.Add(new IdentityRole(role));
//                }
//            }
//            db.SaveChanges();

//            var user = userManager.FindByName(superUser);
//            if (user == null)
//            {
//                user = new ApplicationUser
//                {
//                    UserName = superUser,
//                    Email = superUserEmail,
//                    Status = roleName,
//                    RepId = -1
//                };
//                userManager.Create(user, password);
//                userManager.SetLockoutEnabled(user.Id, false);
//            }

//            // Add user rolename if not already added
//            var rolesForUser = userManager.GetRoles(user.Id);
//            if (!rolesForUser.Contains(roleName))
//            {
//                var result = userManager.AddToRole(user.Id, roleName);
//            }
        }
        class adsys
        {
            private AgilAdsDataContext _context;
            private string _superUser = null;
            private int saID = 0;
            private int spID = 0;
            private int rID = 0;
            private int pID = 0;
            private int bID = 0;
            private int raID = 0;
            private int iID = 0;
            private int mID = 0;
            public adsys(AgilAdsDataContext context)
            {
                _context = context;
            }
            public int addSuperAdmin(Person adminUser)
            {
                int retval = -1;
                if (!String.IsNullOrWhiteSpace(_superUser) &&
                    !String.IsNullOrWhiteSpace(adminUser.Username))
                {
                    //var sae = new SuperAdmin();
                    //sae.id = ++saID;
                    //sae.Username = adminUser.Username;
                    //sae.Expiration = DateTime.Now.AddDays(14);
                    //_context.SuperAdmin.Add(sae);
                    //_context.SaveChanges();
                    //retval = sae.id;
                    //pushUser(adminUser, "SA");
                }
                return retval;
            }
            void pushUser(Person person, string role)
            {
                var user = userManager.FindByName(person.Username);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = person.Username,
                        //Email = person.PrimaryContactMethod,
                        
                        Status= role,
                        RepId = -1
                    };
                    userManager.Create(user, "Firef0x.com");
                    userManager.SetLockoutEnabled(user.Id, false);
                    var rolesForUser = userManager.GetRoles(user.Id);
                    if (!rolesForUser.Contains(role))
                    {
                        var result = userManager.AddToRole(user.Id, role);
                    }
                }
            }
            public int addPriv(string username,
                Valid.Context context, Valid.Action action)
            {
                //var priv = new SuperPriv();
                //priv.Action = action.ToString();
                //priv.Context = context.ToString();
                //priv.id = ++spID;
                //priv.Username = username;
                //_context.SuperPriv.Add(priv);
                //_context.SaveChanges();
                return 0;// spID;
            }
            public int addRep(string region, Person rep, BusinessInfo org)
            {
                int retval = -1;
                {
                    var re = new Rep();
                    re.id = ++rID;
                    re.Region = region;
                    //re.BusinessId = addBusiness(org);
                    //re.IdentityId = addPerson(rep, re.BusinessId);
                    _context.Rep.Add(re);
                    _context.SaveChanges();
                    retval = re.id;
                    pushUser(rep, "Representative");
                }
                return retval;
            }
            public int addPerson(Person p, int biid)
            {
                var indiv = _context.People.FirstOrDefault(i =>
                     i.Fullname.Equals(p.Fullname));
                if (indiv == null)
                {
                    p.id = ++pID;
                    p.BusinessInfoId = biid;
                    _context.People.Add(p);
                    return p.id;
                }
                return 0;// indiv.id;
            }
            public int addBusiness(string name)
            {
                //var business = _context.FirstOrDefault(b =>
                //     b.OrganizationName.Equals(bi.OrganizationName));
                //if (business == null)
                //{
                //    bi.id = ++bID;
                //    _context.Business.Add(bi);
                //    return bi.id;
                //}
                return 0;// business.id;
            }
            public int addRepAdmin(int repID, Person adminUser)
            {
                int retval = -1;
                if (!String.IsNullOrWhiteSpace(adminUser.Username))
                {
                    //var rae = new RepAdmin();
                    //rae.id = ++raID;
                    //rae.RepID = repID;
                    //rae.Username = adminUser.Username;
                    //rae.Expiration = new DateTime().AddDays(14);
                    //_context.RepAdmin.Add(rae);
                    //_context.SaveChanges();
                    //retval = rae.id;
                    //pushUser(adminUser, "RA");
                }
                return retval;
            }
            //public int addInstitution(int ownerID, Person focal, BusinessInfo org)
            //{
            //    int retval = -1;
            //    var ie = new Institution();
            //    ie.id = ++iID;
            //    ie.OwnerID = ownerID;
            //    ie.FocalPoint = focal;
            //    ie.Organization = org;
            //    _context.Institutions.Add(ie);
            //    _context.SaveChanges();
            //    retval = ie.id;
            //    return retval;
            //}
            //public int addMember(int ownerID, Person focal, BusinessInfo org)
            //{
            //    int retval = -1;
            //    var me = new Member();
            //    me.id = ++mID;
            //    me.OwnerID = ownerID;
            //    me.FocalPoint = focal;
            //    me.Organization = org;
            //    _context.Members.Add(me);
            //    _context.SaveChanges();
            //    retval = me.id;
            //    return retval;
            //}
        };
    }
}