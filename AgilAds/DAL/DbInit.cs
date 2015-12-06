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
using System.Data.Entity.Validation;

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
            Helpers.Startup.getConfigDefaults();
            var testdata = new adsys(context);

            userManager = HttpContext
            .Current.GetOwinContext()
            .GetUserManager<ApplicationUserManager>();

            var p = new Person()
            {
                Firstname = "Frazier",
                Lastname = "Young",
                Username = Helpers.Startup.defaultUser,
                Contacts = new List<PersonalContact>()
            };
            var b = new BusinessInfo() { OrganizationName = "AgilAds" };
            var repId = testdata.addRep("Burien WA", p, b);
            testdata.addBusinessContact(repId, new BusinessContact()
            {
                Contact = "123 E 47th St, Burien, WA 98168",
                Method = ContactInfo.contactMethod.address
            });
            testdata.addBusinessContact(repId, new BusinessContact()
            {
                Contact = "agilads@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            testdata.addBusinessContact(repId, new BusinessContact()
            {
                Contact = "agilads@primeSites.com",
                Method = ContactInfo.contactMethod.email
            });
            testdata.addBusinessContact(repId, new BusinessContact()
            {
                Contact = "www.agilads.com",
                Method = ContactInfo.contactMethod.webSite
            });
            testdata.addBusinessContact(repId, new BusinessContact()
            {
                Contact = "206.555.1122",
                Method = ContactInfo.contactMethod.phone
            });
            var teamBurien1 = testdata.addTeamMember(repId, new Person() 
            { 
                Firstname = "Al", 
                Lastname = "Green",
                Contacts = new List<PersonalContact>(),
                Username = "algreen" });
            testdata.addPersonalContact(repId, teamBurien1, new PersonalContact()
            {
                Contact = "206.555.1122",
                Method = ContactInfo.contactMethod.phone
            });
            testdata.addPersonalContact(repId, teamBurien1, new PersonalContact()
            {
                Contact = "agilads@primeSites.com",
                Method = ContactInfo.contactMethod.email
            });
            //var town = testdata.addBusiness(new BusinessInfo());
            //var eslID = testdata.addRep("East St. Louis", new Person(), new BusinessInfo());
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
                    re.Region = region;
                    re.Contacts = new List<BusinessContact>();
                    re.Team = new List<Person>();
                    re.Team.Add(rep);
                    re.FocalPoint = rep;
                    re.OrganizationName = org.OrganizationName;
                    _context.Rep.Add(re);
                    SaveChanges(_context);
                    retval = re.id;
                    pushUser(rep, "RA");
                }
                return retval;
            }
            public void addBusinessContact(int repId, BusinessContact bc)
            {
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    rep.Contacts.Add(bc);
                    SaveChanges(_context);
                }
            }
            public void addPersonalContact(int repId, int personId, PersonalContact pc)
            {
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var person = _context.People.FirstOrDefault(p => p.id.Equals(personId));
                    if (person != null && person.BusinessInfoId.Equals(rep.id))
                    {
                        person.Contacts.Add(pc);
                        SaveChanges(_context);
                    }
                }
            }
            public int addTeamMember(int repId, Person member)
            {
                int retval = -1;
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    rep.Team.Add(member);
                    SaveChanges(_context);
                    retval = member.id;
                }
                return retval;
            }
            public void SaveChanges(AgilAdsDataContext context)
            {
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    context.SaveChanges("DbInit");
                }
                catch (DbEntityValidationException e)
                {
                    throw;
                }
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