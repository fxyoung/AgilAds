using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgilAds.Models
{
    public class SharedEntityCreationModel
    {
        [Required]
        [Display(Name = "Organization Name")]
        [StringLength(Helpers.Constants.orgNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.orgNameMin)]
        [RegularExpression(Helpers.Constants.orgNameRegexPattern)]
        public string OrganizationName { get; set; }
        public ContactInfo.contactMethod OrganiztionContactMethod { get; set; }
        public string OrganizationContact { get; set; }
        [MaxLength(Helpers.Constants.bigBuffer)]
        [Display(Name = "Bank Account Number")]
        public string BankAcctNo { get; set; }
        [Required]
        [Display(Name = "Focal Point First Name")]
        [StringLength(Helpers.Constants.indivNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.indivNameMin)]
        [RegularExpression(Helpers.Constants.indivNameRegexPattern)]
        public string FocalFirstname { get; set; }
        [Required]
        [Display(Name = "Focal Point Last Name")]
        [StringLength(Helpers.Constants.indivNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.indivNameMin)]
        [RegularExpression(Helpers.Constants.indivNameRegexPattern)]
        public string FocalLastname { get; set; }
        [Display(Name = "Focal Point Username")]
        [StringLength(Helpers.Constants.userNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.userNameMin)]
        [RegularExpression(Helpers.Constants.userNameRegexPattern)]
        public string Username { get; set; }
        public ContactInfo.contactMethod FocalContactMethod { get; set; }
        public string FocalContact { get; set; }
    }
}