using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    [Table("Privs")]
    public class Priv
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Username")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9.@$!#%?_]*")]
        public string Username { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z][a-zA-Z]*")]
        public string Context { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z][a-zA-Z]*")]
        public string Action { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Modified { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ModifiedBy { get; set; }
    }
}