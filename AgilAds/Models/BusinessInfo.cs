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
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9.@$!#%?_]*")]
        public string OrganizationName { get; set; }
        [MaxLength(2000)]
        [Display(Name = "Bank Account Number")]
        public string BankAcctNo { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Modified { get; set; }
        [HiddenInput(DisplayValue = false)]
        [MaxLength(25)]
        public string ModifiedBy { get; set; }

        [HiddenInput(DisplayValue = false)]
        public byte[] ArcSum { get; set; }  //checksum
        [HiddenInput(DisplayValue = false)]
        public byte[] Secret { get; set; }  //key

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