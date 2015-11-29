using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgilAds.Models
{
    public class Payment
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int From { get; set; }
        [Required]
        public int To { get; set; }
        [Required]
        public string AccountNo { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}