namespace AgilAds.MigrationsAuditDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbAudits",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 200),
                        RevisionStamp = c.DateTime(nullable: false),
                        Tablename = c.String(maxLength: 50),
                        Propertyname = c.String(maxLength: 50),
                        Username = c.String(maxLength: 25),
                        Action = c.String(maxLength: 1),
                        OldData = c.String(maxLength: 500),
                        NewData = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DbAudits");
        }
    }
}
