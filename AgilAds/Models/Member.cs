using AgilAds.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    public class Member : BusinessInfo
    {
        [MaxLength(Helpers.Constants.bigBuffer)]
        public string StaticMsg { get; set; }

        public virtual ICollection<RepPayment> SURepPayments { get; set; }
        public virtual ICollection<AdInfo> Ads { get; set; }

        public Member() { }

        public Member(MemCreateView template)
        {
            BankAcctNo = template.BankAcctNo;
            OrganizationName = template.OrganizationName;
            var focal = new Person();
            focal.Firstname = template.FocalFirstname;
            focal.Lastname = template.FocalLastname;
            focal.Username = template.Username;
            if (!String.IsNullOrWhiteSpace(template.Username))
            {
                Helpers.Startup.AddUserRole(
                    template.Username,
                    "Member",
                    throwIfUserNotFound: true);
            }
            focal.Contacts = new List<PersonalContact>();
            if (!String.IsNullOrWhiteSpace(template.FocalContact))
            {
                var c = new PersonalContact();
                c.Contact = template.FocalContact;
                c.Method = template.FocalContactMethod;
                focal.Contacts.Add(c);
            }
            Contacts = new List<BusinessContact>();
            if (!String.IsNullOrWhiteSpace(template.OrganizationContact))
            {
                var c = new BusinessContact();
                c.Contact = template.FocalContact;
                c.Method = template.FocalContactMethod;
                Contacts.Add(c);
            }
            //FocalPoint = focal;
            Team = new List<Person>() { focal };
            StaticMsg = template.StaticMsg;
        }
    }
    public class MemListAllView
    {
        public int id { get; set; }
        public string OrganizationName { get; set; }
        public string FocalPoint { get; set; }
        [MaxLength(Helpers.Constants.bigBuffer)]
        public string StaticMsg { get; set; }

        public MemListAllView(Member template)
        {
            id = template.id;
            OrganizationName = template.OrganizationName;
            FocalPoint = AgilAds.Helpers.Utils.FindNameOf(template.FocalPointId);
            StaticMsg = template.StaticMsg;
        }

        public static List<MemListAllView> CreateListView(List<Member> members)
        {
            var retval = new List<MemListAllView>();
            foreach (var member in members)
            {
                retval.Add(new MemListAllView(member));
            }
            return retval;
        }
    }

    public class MemCreateView : SharedEntityCreationModel
    {
        public string StaticMsg { get; set; }
    }
}