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
    public class Institution : BusinessInfo
    {
        [DataType(DataType.Currency)]
        public double MonthlyAdFee { get; set; }

        public Institution() { }

        public Institution(InstitutionCreateView template)
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
            MonthlyAdFee = template.MonthlyAdFee;
        }

        public virtual ICollection<AdInfo> Ads { get; set; }
        public virtual ICollection<InstitutionPayment> Receipts { get; set; }
    }

    public class InstitutionCreateView : SharedEntityCreationModel
    {
        [DataType(DataType.Currency)]
        public double MonthlyAdFee { get; set; }
    }

    public class InstitutionListAllView
    {
        public int id { get; set; }
        public string OrganizationName { get; set; }
        public string FocalPoint { get; set; }
        [DataType(DataType.Currency)]
        public double MonthlyAdFee { get; set; }

        public InstitutionListAllView(Institution template)
        {
            id = template.id;
            OrganizationName = template.OrganizationName;
            FocalPoint = AgilAds.Helpers.Utils.FindNameOf(template.FocalPointId);
            MonthlyAdFee = template.MonthlyAdFee;
        }

        public static List<InstitutionListAllView> CreateListView(List<Institution> institutions)
        {
            var retval = new List<InstitutionListAllView>();
            foreach (var institution in institutions)
            {
                retval.Add(new InstitutionListAllView(institution));
            }
            return retval;
        }
    }
}