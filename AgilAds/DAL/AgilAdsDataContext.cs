using AgilAds.DAL.Audit;
using AgilAds.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        public virtual DbSet<Priv> Priv { get; set; }
        public virtual DbSet<Rep> Rep { get; set; }

        public System.Data.Entity.DbSet<AgilAds.Models.Person> People { get; set; }

        public int SaveChanges(string username)
        {
            var now = DateTime.Now;
            foreach (DbEntityEntry entity in
                     ChangeTracker.Entries().Where(e => 
                         e.State == EntityState.Modified ||
                         e.State == EntityState.Added ||
                         e.State == EntityState.Deleted))
            {
                var newData = new Dictionary<string, object>();
                var oldData = new Dictionary<string, object>();
                var auditRecord = new DbAudit()
                {
                    id = Guid.NewGuid().ToString(),
                    Action = entity.State.ToString(),
                    RevisionStamp = now,
                    Tablename = entity.Entity.ToString(),
                    Username = username
                };
                foreach (var propName in entity.CurrentValues.PropertyNames)
                {
                    newData.Add (propName, entity.CurrentValues[propName]);
                    if (!entity.State.Equals(EntityState.Added))
                    {
                        oldData.Add(propName, entity.OriginalValues[propName]);
                    }
                }
                auditRecord.OldData = JsonConvert.SerializeObject(oldData);
                auditRecord.NewData = JsonConvert.SerializeObject(newData);
                using(var auditDB = new AuditDataContext())
                {
                    auditDB.AuditTrail.Add(auditRecord);
                    auditDB.SaveChanges();
                }
            }
            return base.SaveChanges();
        }
    }
}