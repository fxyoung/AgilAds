using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    public class Person
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int BusinessInfoId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Fullname
        {
            get { return Lastname + ", " + Firstname; }
            private set { /* needed for EF */ }
        }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z][a-zA-Z]*")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z][a-zA-Z]*")]
        public string Lastname { get; set; }
        [Display(Name = "Username")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9.@$!#%?_]*")]
        public string Username { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Modified { get; set; }
        [HiddenInput(DisplayValue = false)]
        [MaxLength(25)]
        public string ModifiedBy { get; set; }

        public virtual ICollection<PersonContactInfo> Contacts { get; set; }
    }
}