using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgilAds.Models
{
    [Table("ContactInfoes")]
    public abstract class ContactInfo
    {
        public enum contactMethod { phone, email, address, webSite, sms }
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Contact Method")]
        public contactMethod Method { get; set; }
        [Required]
        [MaxLength(Helpers.Constants.bigBuffer)]
        [Display(Name = "Contact Information")]
        public string Contact { get; set; }
    }
    public class PersonalContact : ContactInfo
    {
        [Required]
        public int IdentityId { get; set; }
        [ForeignKey("IdentityId")]
        public virtual Person Identity { get; set; }
    }
    public class BusinessContact : ContactInfo
    {
        [Required]
        public int BusinessInfoId { get; set; }
        [ForeignKey("BusinessInfoId")]
        public virtual BusinessInfo BusinessInfo { get; set; }
    }
}