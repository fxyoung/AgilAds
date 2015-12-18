using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilAds.Models
{
    public class Institution : BusinessInfo
    {
        //[Key]
        //public int id { get; set; }
        [DataType(DataType.Currency)]
        public double MonthlyAdFee { get; set; }

        public virtual ICollection<AdInfo> Ads { get; set; }
        public virtual ICollection<InstitutionPayment> Receipts { get; set; }
    }
}