using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    public class Rep : BusinessInfo
    {
        //[Key]
        //public int id { get; set; }
        [StringLength(Helpers.Constants.regionNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.regionNameMin)]
        [RegularExpression(Helpers.Constants.regionNameRegexPattern)]
        public string Region { get; set; }
        [DataType(DataType.Currency)]
        public double Fee { get; set; }
        public double TaxRate { get; set; }
        
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<RepPayment> Receipts { get; set; }

        public Rep() { }
        public Rep(RepCreateView template)
        {
            var focal = new Person();
            focal.Firstname = template.FocalFirstname;
            focal.Lastname = template.FocalLastname ;
            focal.Username = template.Username;
            if (!String.IsNullOrWhiteSpace(template.Username))
            {
                Helpers.Startup.AddUserRole(
                    template.Username, 
                    "Representative", 
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
            OrganizationName = template.OrganizationName;
            Contacts = new List<BusinessContact>();
            if (!String.IsNullOrWhiteSpace(template.OrganizationContact))
            {
                var c = new BusinessContact();
                c.Contact = template.FocalContact;
                c.Method = template.FocalContactMethod;
                Contacts.Add(c);
            }
            Fee = template.Fee;
            TaxRate = template.TaxRate;
            Region = template.Region;
            // = focal;
            Team = new List<Person>() { focal };
        }
    }
    public class RepListAllView
    {
        public int id { get; set; }
        public string Region { get; set; }
        public string FocalPoint { get; set; }
        public string Orgname { get; set; }

        public RepListAllView(Rep template)
        {
            id = template.id;
            Region = template.Region;
            FocalPoint = null; ;// template.FocalPoint.Username;
            Orgname = template.OrganizationName;
        }
    }

    public class RepCreateView : SharedEntityCreationModel
    {
        [StringLength(Helpers.Constants.regionNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.regionNameMin)]
        [RegularExpression(Helpers.Constants.regionNameRegexPattern)]
        public string Region { get; set; }
        [DataType(DataType.Currency)]
        public double Fee { get; set; }
        public double TaxRate { get; set; }
    }
}