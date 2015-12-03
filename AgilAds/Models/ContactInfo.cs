using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgilAds.Models
{
    public enum contactMethod { phone, email, address, webSite, sms }
    public abstract class ContactInfo
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Contact Method")]
        public contactMethod Method { get; set; }
        [Required]
        [MaxLength(2000)]
        [Display(Name = "Contact Information")]
        public string Contact { get; set; }
    }
    [Table("PersonalContacts")]
    public class PersonalContact : ContactInfo
    {
        [Required]
        public int IdentityId { get; set; }
        [ForeignKey("IdentityId")]
        public virtual Person Identity { get; set; }
    }
    [Table("BusinessContacts")]
    public class BusinessContact : ContactInfo
    {
        [Required]
        public int BusinessInfoId { get; set; }
        [ForeignKey("BusinessInfoId")]
        public virtual BusinessInfo BusinessInfo { get; set; }
    }
}