﻿using AgilAds.Attributes;
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
        [StringLength(Helpers.Constants.userNameMax, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Helpers.Constants.userNameMin)]
        [RegularExpression(Helpers.Constants.userNameRegexPattern)]
        public string Username { get; set; }
        [Required]
        public Valid.Context Context { get; set; }
        [Required]
        public Valid.Action Action { get; set; }
    }
}