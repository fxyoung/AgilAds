using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    [Table("Members")]
    public class Member : BusinessInfo
    {
        //[Key]
        //public int id { get; set; }
        [Required]
        public int FocalPointId { get; set; } //restricted to team members
        [ForeignKey("FocalPointId")]
        public virtual Person FocalPoint { get; set; }
        //[Required]
        //public int OrganizationId { get; set; }
        //[ForeignKey("OrganizationId")]
        //public virtual BusinessInfo Organization { get; set; }
        public string StaticMsg { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public DateTime Modified { get; set; }
        //[HiddenInput(DisplayValue = false)]
        //[MaxLength(25)]
        //public string ModifiedBy { get; set; }

        public virtual ICollection<RepPayment> RepPayments { get; set; }
        public virtual ICollection<InstitutionPayment> InstitutionPayments { get; set; }
        public virtual ICollection<AdInfo> Ads { get; set; }
    }
}