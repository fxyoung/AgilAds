using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    public enum reqStatus { request, accept, reject }
    public class AdInfo
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int InstitutionID { get; set; }
        [Required]
        public int MemberID { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [MaxLength(2000)]
        public string DynamicResourceURL { get; set; }
        [Required]
        public reqStatus ReqStatus { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Modified { get; set; }
        [HiddenInput(DisplayValue = false)]
        [MaxLength(25)]
        public string ModifiedBy { get; set; }

        //[ForeignKey("InstitutionID")]
        //public virtual Institution Institution { get; set; }
        //[ForeignKey("MemberID")]
        //public virtual Member Member { get; set; }
        //public virtual ICollection<InstitutionalCharges> InstSurcharges { get; set; }
        //public virtual ICollection<SystemicCharges> SystemSurcharges { get; set; }
        //public virtual ICollection<Rating> UserComments { get; set; }
    }
}