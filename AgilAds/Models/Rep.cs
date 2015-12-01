using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    public class Rep
    {
        [Key]
        public int id { get; set; }
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9]*")]
        public string Region { get; set; }
        [Required]
        public int IdentityId { get; set; }
        public int BusinessId { get; set; }
        [DataType(DataType.Currency)]
        public double Fee { get; set; }
        public double TaxRate { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public DateTime Modified { get; set; }
        [HiddenInput(DisplayValue = false)]
        [MaxLength(25)]
        public string ModifiedBy { get; set; }

        [ForeignKey("IdentityId")]
        public virtual Person Identity { get; set; }
        [ForeignKey("BusinessId")]
        public virtual BusinessInfo Business { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
    }
}