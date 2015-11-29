namespace AgilAds.MigrationsAgilAdsDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppPayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        From = c.Int(nullable: false),
                        To = c.Int(nullable: false),
                        AccountNo = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.People", "BusinessInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.BusinessInfoes", "BankAcctNo", c => c.String(maxLength: 2000));
            AddColumn("dbo.BusinessInfoes", "ArcSum", c => c.Binary());
            AddColumn("dbo.BusinessInfoes", "Secret", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BusinessInfoes", "Secret");
            DropColumn("dbo.BusinessInfoes", "ArcSum");
            DropColumn("dbo.BusinessInfoes", "BankAcctNo");
            DropColumn("dbo.People", "BusinessInfoId");
            DropTable("dbo.Payments");
        }
    }
}
