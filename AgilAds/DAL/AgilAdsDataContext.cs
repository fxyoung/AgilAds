using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgilAds.DAL
{
    public class AgilAdsDataContext : DbContext
    {
        public AgilAdsDataContext()
            : base("AgilAdsConnection") { }
        //public virtual DbSet<AdInfo> AdInfo { get; set; }
        //public virtual DbSet<Admin> Admin { get; set; }
        //public virtual DbSet<BusinessInfo> BusinessInfo { get; set; }
        //public virtual DbSet<BusinessInfoContactInfo> BusinessInfoContactInfo { get; set; }
        //public virtual DbSet<PersonContactInfo> PersonContactInfo { get; set; }
        //public virtual DbSet<Institution> Institution { get; set; }
        //public virtual DbSet<Member> Member { get; set; }
        //public virtual DbSet<Person> Person { get; set; }
        //public virtual DbSet<Priv> Priv { get; set; }
        public virtual DbSet<Rep> Rep { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }

        public System.Data.Entity.DbSet<AgilAds.Models.Person> People { get; set; }
    }
}