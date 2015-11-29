namespace AgilAds.MigrationsAgilAdsDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFieldFormats : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reps", "Region", c => c.String(maxLength: 25));
            AlterColumn("dbo.Reps", "ModifiedBy", c => c.String(maxLength: 25));
            AlterColumn("dbo.Admins", "ModifiedBy", c => c.String(maxLength: 25));
            AlterColumn("dbo.People", "Firstname", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.People", "Lastname", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.People", "Username", c => c.String(maxLength: 25));
            AlterColumn("dbo.People", "ModifiedBy", c => c.String(maxLength: 25));
            AlterColumn("dbo.PersonContactInfoes", "Contact", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Institutions", "ModifiedBy", c => c.String(maxLength: 25));
            AlterColumn("dbo.AdInfoes", "DynamicResourceURL", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AdInfoes", "ModifiedBy", c => c.String(maxLength: 25));
            AlterColumn("dbo.BusinessInfoes", "OrganizationName", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("dbo.BusinessInfoes", "ModifiedBy", c => c.String(maxLength: 25));
            AlterColumn("dbo.Members", "ModifiedBy", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "ModifiedBy", c => c.String());
            AlterColumn("dbo.BusinessInfoes", "ModifiedBy", c => c.String());
            AlterColumn("dbo.BusinessInfoes", "OrganizationName", c => c.String(nullable: false));
            AlterColumn("dbo.AdInfoes", "ModifiedBy", c => c.String());
            AlterColumn("dbo.AdInfoes", "DynamicResourceURL", c => c.String());
            AlterColumn("dbo.Institutions", "ModifiedBy", c => c.String());
            AlterColumn("dbo.PersonContactInfoes", "Contact", c => c.String(nullable: false));
            AlterColumn("dbo.People", "ModifiedBy", c => c.String());
            AlterColumn("dbo.People", "Username", c => c.String());
            AlterColumn("dbo.People", "Lastname", c => c.String(nullable: false));
            AlterColumn("dbo.People", "Firstname", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Reps", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Reps", "Region", c => c.String());
        }
    }
}
