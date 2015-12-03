using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int IdentityId { get; set; }
        public DateTime? Expiration { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Modified { get; set; }
        [HiddenInput(DisplayValue = false)]
        [MaxLength(25)]
        public string ModifiedBy { get; set; }

        [ForeignKey("IdentityId")]
        public virtual Person Identity { get; set; }
    }
}