using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgilAds.Models
{
    public class DbAudit
    {
        [MaxLength(200)]
        [Key]
        public string id { get; set; }
        public DateTime RevisionStamp { get; set; }
        [MaxLength(50)]
        public string Tablename { get; set; }
        [Display(Name = "Username")]
        [StringLength(Helpers.Constants.userNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.userNameMin)]
        [RegularExpression(Helpers.Constants.userNameRegexPattern)]
        public string Username { get; set; }
        [MaxLength(10)]
        public string Action { get; set; }
        [MaxLength(500)]
        public string OldData { get; set; }
        [MaxLength(500)]
        public string NewData { get; set; }
    }
}