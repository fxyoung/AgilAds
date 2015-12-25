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

        protected override void Seed(AgilAdsDataContext context)
        {
            base.Seed(context);
            Helpers.Startup.getConfigDefaults();
            var testdata = new adsys(context);

            var p = new Person()
            {
                Firstname = "Frazier",
                Lastname = "Young",
                Username = Helpers.Startup.defaultUser,
                Contacts = new List<PersonalContact>()
                {
                    new PersonalContact()
                    {
                        Method=ContactInfo.contactMethod.email,
                        Contact="fxyoung@hotmail.com"
                    }
                }
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
            var teamBurien1algreen = testdata.addTeamMember(repId, new Person() 
            { 
                Firstname = "Al", 
                Lastname = "Green",
                Contacts = new List<PersonalContact>(),
                Username = "algreen" 
            },"RA");
            testdata.addPersonalContact(repId, teamBurien1algreen, new PersonalContact()
            {
                Contact = "206.555.1122",
                Method = ContactInfo.contactMethod.phone
            });
            testdata.addPersonalContact(repId, teamBurien1algreen, new PersonalContact()
            {
                Contact = "agilads@primeSites.com",
                Method = ContactInfo.contactMethod.email
            });
            var bi = new BusinessInfo() { OrganizationName = "Al's Emporium" };
            var pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.email, 
                            Contact="foo@bar.com"}};
            p = new Person(){Firstname="Al", Lastname="Johnstone", Contacts=pc, Username="aljohnstone"};
            var memAl = testdata.addMemberOrg(repId, p, bi);
            testdata.addBusinessContact(memAl, new BusinessContact()
            {
                Contact = "alempo@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            bi = new BusinessInfo() { OrganizationName = "Hot Shot Beauty Salon" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="1-800-555-1212"}};
            p = new Person() { Firstname = "Mabel", Lastname = "Smith", Contacts = pc, Username="mabelsmith" };
            var memMabel = testdata.addMemberOrg(repId, p, bi);
            testdata.addBusinessContact(memMabel, new BusinessContact()
            {
                Contact = "hotshot@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            bi = new BusinessInfo() { OrganizationName = "The Hat Store" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.email, 
                            Contact="missy@hats.com"}};
            p = new Person() { Firstname = "Missy", Lastname = "Fine", Contacts = pc, Username="missyfine" };
            var memMissy = testdata.addMemberOrg(repId, p, bi);
            testdata.addBusinessContact(memMissy, new BusinessContact()
            {
                Contact = "hatstore@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            bi = new BusinessInfo() { OrganizationName = "Sue Hu's Pool Hall" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.sms, 
                            Contact="1-888-555-1234"}};
            p = new Person() { Firstname = "Sue", Lastname = "Hu", Contacts = pc };
            p.Username = (p.Firstname + "_" + p.Lastname).ToLower();
            var memSue = testdata.addMemberOrg(repId, p, bi);
            testdata.addBusinessContact(memSue, new BusinessContact()
            {
                Contact = "hupool@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            bi = new BusinessInfo() { OrganizationName = "Jake's Auto Repair" };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.email, 
                            Contact="jake@memphis.com"}};
            p = new Person() { Firstname = "Jake", Lastname = "Memphis", Contacts = pc };
            p.Username = (p.Firstname + p.Lastname).ToLower();
            var memJake = testdata.addMemberOrg(repId, p, bi);
            testdata.addBusinessContact(memJake, new BusinessContact()
            {
                Contact = "autojake@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            var bc = new List<BusinessContact>(){ new BusinessContact(){
                            Method=AgilAds.Models.ContactInfo.contactMethod.address, 
                            Contact="3233 W 47th Blvd, Seahurst, Wa 98145"}};
            bi = new BusinessInfo() { OrganizationName = "Burien Chamber of Commerce", Contacts = bc };
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="206--555-4321 ext 4423"}};
            p = new Person() { Firstname = "Maria", Lastname = "Jones", Contacts = pc };
            p.Username = (p.Firstname + p.Lastname).ToLower();
            var insBCOC = testdata.addInstitution(repId, p, bi);
            testdata.addBusinessContact(insBCOC, new BusinessContact()
            {
                Contact = "BCOC@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="206--555-4321 ext 4427"}};
            p = new Person() { Firstname = "Foster", Lastname = "Wright", Contacts = pc };
            p.Username = (p.Firstname + p.Lastname).ToLower();
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
            p.Username = (p.Firstname + p.Lastname).ToLower();
            var insSSCCSOB = testdata.addInstitution(repId, p, bi);
            testdata.addBusinessContact(insSSCCSOB, new BusinessContact()
            {
                Contact = "SSCCSOB@gmail.com",
                Method = ContactInfo.contactMethod.email
            });
            pc = new List<PersonalContact>() { new PersonalContact(){ 
                            Method=AgilAds.Models.ContactInfo.contactMethod.phone, 
                            Contact="206--555-2805 ext 427"}};
            p = new Person() { Firstname = "Marty", Lastname = "Frank", Contacts = pc };
            p.Username = (p.Firstname + p.Lastname).ToLower();
            var insSSCCSOBtm1 = testdata.addInsTeamMember(repId, insSSCCSOB, p);
        }
        class adsys
        {
            private AgilAdsDataContext _context;
            private ApplicationUserManager userManager;
            public adsys(AgilAdsDataContext context)
            {
                _context = context;
                userManager = HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();

            }
            void pushUser(Person person, string role)
            {
                var user = userManager.FindByName(person.Username);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = person.Username,
                        Status= role,
                        Email = GetEmail(person),
                        RepId = -1
                    };
                    var idRes = userManager.Create(user, "Firef0x.com");
                    userManager.SetLockoutEnabled(user.Id, false);
                    var rolesForUser = userManager.GetRoles(user.Id);
                    if (!rolesForUser.Contains(role))
                    {
                        var result = userManager.AddToRole(user.Id, role);
                    }
                }
            }

            private string GetEmail(Person person)
            {
                var email = person.Firstname + "@" + person.Lastname + ".com";
                foreach(var contact in person.Contacts)
                {
                    if (contact.Method.Equals(ContactInfo.contactMethod.email))
                    {
                        email = contact.Contact;
                        break;
                    }
                }
                return email;
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
                    re.Members = new List<Member>();
                    re.Institutions = new List<Institution>();
                    re.ParentId = null;
                    re.Team.Add(rep);
                    re.OrganizationName = truncateOrgName(org.OrganizationName);
                    _context.Reps.Add(re);
                    SaveChanges(_context);
                    re.FocalPointId = rep.id;
                    SaveChanges(_context);
                    retval = re.id;
                    pushUser(rep, "Representative");
                }
                return retval;
            }
            public void addBusinessContact(int repId, BusinessContact bc)
            {
                var bi = _context.BusinessInfoes.FirstOrDefault(r => r.id.Equals(repId));
                if (bi != null)
                {
                    bi.Contacts.Add(bc);
                    SaveChanges(_context);
                }
            }
            public void addPersonalContact(int repId, int personId, PersonalContact pc)
            {
                var rep = _context.Reps.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var person = rep.Team.FirstOrDefault(p => p.id.Equals(personId));
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
                var rep = _context.Reps.FirstOrDefault(r => r.id.Equals(repId));
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
                var rep = _context.Reps.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var mem = new Member();
                    mem.Contacts = new List<BusinessContact>();
                    mem.Team = new List<Person>();
                    mem.Team.Add(focal);
                    mem.ParentId = repId;
                    mem.OrganizationName = truncateOrgName(org.OrganizationName);
                    rep.Members.Add(mem);
                    SaveChanges(_context);
                    mem.FocalPointId = focal.id;
                    SaveChanges(_context);
                    retval = mem.id;
                    pushUser(focal, "Member");
                }
                return retval;
            }
            public int addMemTeamMember(int repId, int memId, Person member)
            {
                int retval = -1;
                var rep = _context.Reps.FirstOrDefault(r => r.id.Equals(repId));
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
                var rep = _context.Reps.FirstOrDefault(r => r.id.Equals(repId));
                if (rep != null)
                {
                    var ins = new Institution();
                    ins.Contacts = new List<BusinessContact>();
                    ins.Team = new List<Person>();
                    ins.Team.Add(focal);
                    ins.OrganizationName = truncateOrgName(org.OrganizationName);
                    ins.ParentId = repId;
                    rep.Institutions.Add(ins);
                    SaveChanges(_context);
                    ins.FocalPointId = focal.id;
                    SaveChanges(_context);
                    retval = ins.id;
                    pushUser(focal, "Institution");
                }
                return retval;
            }
            string truncateOrgName (string orgName)
            {
                if (orgName.Length < Helpers.Constants.orgNameMax) return orgName;
                else return orgName.Substring(0, Helpers.Constants.orgNameMax);
            }
            public int addInsTeamMember(int repId, int insId, Person member)
            {
                int retval = -1;
                var rep = _context.Reps.FirstOrDefault(r => r.id.Equals(repId));
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
                    var ex = e;
                    throw e;
                }
            }
        };
    }
}