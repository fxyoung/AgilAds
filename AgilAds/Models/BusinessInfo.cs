using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    [Table("BusinessInfoes")]
    public class BusinessInfo
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Organization Name")]
        [StringLength(Helpers.Constants.orgNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.orgNameMin)]
        [RegularExpression(Helpers.Constants.orgNameRegexPattern)]
        public string OrganizationName { get; set; }
        [MaxLength(Helpers.Constants.bigBuffer)]
        [Display(Name = "Bank Account Number")]
        public string BankAcctNo { get; set; }
        [Required]
        public int FocalPointId { get; set; } //restricted to team members

        [HiddenInput(DisplayValue = false)]
        public byte[] ArcSum { get; set; }  //checksum
        [HiddenInput(DisplayValue = false)]
        public byte[] Secret { get; set; }  //key

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public BusinessInfo Parent { get; set; }

        public virtual ICollection<Person> Team { get; set; }
        public virtual ICollection<BusinessContact> Contacts { get; set; }

        public bool IsValid()
        {
            return ArcSum.SequenceEqual(HMAC());
        }
        private byte[] HMAC()
        {
            return null;
        }
    }
}