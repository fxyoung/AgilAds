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
        public string StaticMsg { get; set; }

        public virtual ICollection<RepPayment> SURepPayments { get; set; }
        public virtual ICollection<AdInfo> Ads { get; set; }
    }
}