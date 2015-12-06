using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgilAds.DAL.Audit
{
    public class AuditDataContext : DbContext
    {
        public AuditDataContext()
            : base("AgilAdsConnection") { }
        public DbSet<DbAudit> AuditTrail { get; set; }
    }
}