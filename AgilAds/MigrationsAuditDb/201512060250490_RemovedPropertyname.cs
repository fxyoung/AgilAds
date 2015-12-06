namespace AgilAds.MigrationsAuditDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPropertyname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DbAudits", "Action", c => c.String(maxLength: 10));
            DropColumn("dbo.DbAudits", "Propertyname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbAudits", "Propertyname", c => c.String(maxLength: 50));
            AlterColumn("dbo.DbAudits", "Action", c => c.String(maxLength: 1));
        }
    }
}
