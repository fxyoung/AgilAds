using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    [Table("Reps")]
    public class Rep : BusinessInfo
    {
        //[Key]
        //public int id { get; set; }
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9 ]*")]
        public string Region { get; set; }
        [Required]
        public int FocalPointId { get; set; } //restricted to team members
        [DataType(DataType.Currency)]
        public double Fee { get; set; }
        public double TaxRate { get; set; }
        
        [ForeignKey("FocalPointId")]
        public virtual Person FocalPoint { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<RepPayment> Receipts { get; set; }
    }
}