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
    public class PersonContactInfo
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int IdentityId { get; set; }
        [Required]
        public contactMethod Method { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Contact { get; set; }

        //[ForeignKey("IdentityId")]
        //public virtual Person Identity { get; set; }
    }
    public class BusinessInfoContactInfo
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int BusinessInfoId { get; set; }
        [Required]
        public contactMethod Method { get; set; }
        [Required]
        public string Contact { get; set; }

        //[ForeignKey("BusinessInfoId")]
        //public virtual BusinessInfo BusinessInfo { get; set; }
    }
}