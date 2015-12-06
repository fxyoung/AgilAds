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
                Username = "algreen" },"RA");
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
            var bi = new BusinessInfo() { OrganizationName = "Al's Emporium" };
            var pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.email, 
                            Contact="foo@bar.com"}};
            p = new Person(){Firstname="Al", Lastname="Johnstone", Contacts=pc};
            var memAl = testdata.addMemberOrg(repId, p, bi);
            bi = new BusinessInfo() { OrganizationName = "Hot Shot Beauty Salon" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="1-800-555-1212"}};
            p = new Person() { Firstname = "Mabel", Lastname = "Smith", Contacts = pc };
            var memMabel = testdata.addMemberOrg(repId, p, bi);
            bi = new BusinessInfo() { OrganizationName = "The Hat Store" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.email, 
                            Contact="missy@hats.com"}};
            p = new Person() { Firstname = "Missy", Lastname = "Fine", Contacts = pc };
            var memMissy = testdata.addMemberOrg(repId, p, bi);
            bi = new BusinessInfo() { OrganizationName = "Sue Hu Pool Hall" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.sms, 
                            Contact="1-888-555-1234"}};
            p = new Person() { Firstname = "Sue", Lastname = "Hu", Contacts = pc };
            var memSue = testdata.addMemberOrg(repId, p, bi);
            bi = new BusinessInfo() { OrganizationName = "Jake's Auto Repair" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.email, 
                            Contact="jake@memphis.com"}};
            p = new Person() { Firstname = "Jake", Lastname = "Memphis", Contacts = pc };
            var memJake = testdata.addMemberOrg(repId, p, bi);
            var bc = new List<BusinessContact>(){ new BusinessContact(){
                            Method=AgilAds.Models.ContactInfo.contactMethod.address, 
                            Contact="3233 W 47th Blvd, Seahurst, Wa 98145"}};
            bi = new BusinessInfo() { OrganizationName = "Burien Chamber of Commerce", Contacts = bc };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="206--555-4321 ext 4423"}};
            p = new Person() { Firstname = "Maria", Lastname = "Jones", Contacts = pc };
            var insBCOC = testdata.addInstitution(repId, p, bi);
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="206--555-4321 ext 4427"}};
            p = new Person() { Firstname = "Foster", Lastname = "Wright", Contacts = pc };
            var insBCOCtm1 = testdata.addInsTeamMember(repId, insBCOC, p);
            bc = new List<BusinessContact>(){ new BusinessContact(){
                            Method=AgilAds.Models.ContactInfo.contactMethod.address, 
                            Contact="334 Ridgeway, Des Moines, WA 98136"},
                            new BusinessContact(){
                                Method=AgilAds.Models.ContactInfo.contactMethod.phone,
                                Contact="206-555-9876"
                            }
            };
            bi = new BusinessInfo() { OrganizationName = "South Seattle Community College School of Business", Contacts = bc };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="206-555-2805 ext 23"}};
            p = new Person() { Firstname = "Paul", Lastname = "Ingles", Contacts = pc };
            var insSSCCSOB = testdata.addInstitution(repId, p, bi);
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="206--555-2805 ext 427"}};
            p = new Person() { Firstname = "Marty", Lastname = "Frank", Contacts = pc };
            var insSSCCSOBtm1 = testdata.addInsTeamMember(repId, insBCOC, p);
        }
        class adsys
        {
            private AgilAdsDataContext _context;
            public adsys(AgilAdsDataContext context)
            {
                _context = context;
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
                    pushUser(rep, "Representative");
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
            public int addTeamMember(int repId, Person member, string role)
            {
                int retval = -1;
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    rep.Team.Add(member);
                    SaveChanges(_context);
                    retval = member.id;
                    pushUser(member, role);
                }
                return retval;
            }
            public int addMemberOrg(int repId, Person focal, BusinessInfo org)
            {
                int retval = -1;
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var mem = new Member();
                    mem.Contacts = new List<BusinessContact>();
                    mem.Team = new List<Person>();
                    mem.Team.Add(focal);
                    mem.FocalPoint = focal;
                    mem.OrganizationName = org.OrganizationName;
                    rep.Members.Add(mem);
                    SaveChanges(_context);
                    retval = mem.id;
                    pushUser(focal, "Member");
                }
                return retval;
            }
            public int addMemTeamMember(int repId, int memId, Person member)
            {
                int retval = -1;
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var mem = rep.Members.FirstOrDefault(m => m.id.Equals(memId));
                    if (mem != null)
                    {
                        mem.Team.Add(member);
                        SaveChanges(_context);
                        retval = member.id;
                        pushUser(member, "Member");
                    }
                }
                return retval;
            }
            public int addInstitution(int repId, Person focal, BusinessInfo org)
            {
                int retval = -1;
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var ins = new Institution();
                    ins.Contacts = new List<BusinessContact>();
                    ins.Team = new List<Person>();
                    ins.Team.Add(focal);
                    ins.FocalPoint = focal;
                    ins.OrganizationName = org.OrganizationName;
                    rep.Institutions.Add(ins);
                    SaveChanges(_context);
                    retval = ins.id;
                    pushUser(focal, "Institution");
                }
                return retval;
            }
            public int addInsTeamMember(int repId, int insId, Person member)
            {
                int retval = -1;
                var rep = _context.Rep.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var ins = rep.Institutions.FirstOrDefault(m => m.id.Equals(insId));
                    if (ins != null)
                    {
                        ins.Team.Add(member);
                        SaveChanges(_context);
                        retval = member.id;
                        pushUser(member, "Institution");
                    }
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
                    throw e;
                }
            }
        };
    }
}