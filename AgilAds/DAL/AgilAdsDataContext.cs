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
        public virtual DbSet<AdInfo> AdInfoes { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BusinessInfo> BusinessInfoes { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Priv> Privs { get; set; }
        public virtual DbSet<Rep> Reps { get; set; }

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