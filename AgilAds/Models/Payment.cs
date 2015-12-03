using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgilAds.Models
{
    [Table("Payments")]
    public abstract class Payment
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int From { get; set; }
        [ForeignKey("From")]
        public virtual Member Member { get; set; }
        [Required]
        public string AccountNo { get; set; }
        [Required]
        public double Amount { get; set; }
    }
    [Table("InstitutionPayments")]
    public class InstitutionPayment : Payment
    {
        [Required]
        public int To { get; set; }
        [ForeignKey("To")]
        public virtual Institution Institution { get; set; }
    }
    [Table("RepPayments")]
    public class RepPayment : Payment
    {
        [Required]
        public int To { get; set; }
        [ForeignKey("To")]
        public virtual Rep Rep { get; set; }
    }
}