namespace AgilAds.MigrationsAgilAdsDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedModifiedInfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "Modified");
            DropColumn("dbo.People", "ModifiedBy");
            DropColumn("dbo.BusinessInfoes", "Modified");
            DropColumn("dbo.BusinessInfoes", "ModifiedBy");
            DropColumn("dbo.AdInfoes", "Modified");
            DropColumn("dbo.AdInfoes", "ModifiedBy");
            DropColumn("dbo.Admins", "Modified");
            DropColumn("dbo.Admins", "ModifiedBy");
            DropColumn("dbo.Privs", "Modified");
            DropColumn("dbo.Privs", "ModifiedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Privs", "ModifiedBy", c => c.String());
            AddColumn("dbo.Privs", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Admins", "ModifiedBy", c => c.String(maxLength: 25));
            AddColumn("dbo.Admins", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.AdInfoes", "ModifiedBy", c => c.String(maxLength: 25));
            AddColumn("dbo.AdInfoes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.BusinessInfoes", "ModifiedBy", c => c.String(maxLength: 25));
            AddColumn("dbo.BusinessInfoes", "Modified", c => c.DateTime(nullable: false));
            AddColumn("dbo.People", "ModifiedBy", c => c.String(maxLength: 25));
            AddColumn("dbo.People", "Modified", c => c.DateTime(nullable: false));
        }
    }
}
