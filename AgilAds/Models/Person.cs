using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    [Table("People")]
    public class Person
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int BusinessInfoId { get; set; }
        [ForeignKey("BusinessInfoId")]
        public virtual BusinessInfo Business { get; set; }
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
        [StringLength(Helpers.Constants.userNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.userNameMin)]
        [RegularExpression(Helpers.Constants.userNameRegexPattern)]
        public string Username { get; set; }

        public virtual ICollection<PersonalContact> Contacts { get; set; }
    }
}