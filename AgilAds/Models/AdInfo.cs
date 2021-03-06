﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    public enum reqStatus { request, accept, reject }
    [Table("AdInfoes")]
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
        [MaxLength(Helpers.Constants.bigBuffer)]
        public string DynamicResourceURL { get; set; }
        [Required]
        public reqStatus ReqStatus { get; set; }

        [ForeignKey("InstitutionID")]
        public virtual Institution Institution { get; set; }
        [ForeignKey("MemberID")]
        public virtual Member Member { get; set; }
        //public virtual ICollection<InstitutionalCharges> InstSurcharges { get; set; }
        //public virtual ICollection<SystemicCharges> SystemSurcharges { get; set; }
        //public virtual ICollection<Rating> UserComments { get; set; }
    }
}