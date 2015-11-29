namespace AgilAds.MigrationsAgilAdsDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Region = c.String(),
                        IdentityId = c.Int(nullable: false),
                        Fee = c.Double(nullable: false),
                        TaxRate = c.Double(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.People", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        Expiration = c.DateTime(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Rep_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.People", t => t.IdentityId, cascadeDelete: true)
                .ForeignKey("dbo.Reps", t => t.Rep_id)
                .Index(t => t.IdentityId)
                .Index(t => t.Rep_id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Username = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PersonContactInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        Method = c.Int(nullable: false),
                        Contact = c.String(nullable: false),
                        Person_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.People", t => t.Person_id)
                .Index(t => t.Person_id);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FocalPointId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        MonthlyAdFee = c.Double(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Rep_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.People", t => t.FocalPointId, cascadeDelete: true)
                .ForeignKey("dbo.BusinessInfoes", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.Reps", t => t.Rep_id)
                .Index(t => t.FocalPointId)
                .Index(t => t.OrganizationId)
                .Index(t => t.Rep_id);
            
            CreateTable(
                "dbo.AdInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        InstitutionID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Expiration = c.DateTime(nullable: false),
                        DynamicResourceURL = c.String(),
                        ReqStatus = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Institutions", t => t.InstitutionID, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberID, cascadeDelete: true)
                .Index(t => t.InstitutionID)
                .Index(t => t.MemberID);
            
            CreateTable(
                "dbo.BusinessInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        OrganizationName = c.String(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.BusinessInfoContactInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BusinessInfoId = c.Int(nullable: false),
                        Method = c.Int(nullable: false),
                        Contact = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusinessInfoes", t => t.BusinessInfoId, cascadeDelete: true)
                .Index(t => t.BusinessInfoId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FocalPointId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        StaticMsg = c.String(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Rep_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.People", t => t.FocalPointId, cascadeDelete: false)
                .ForeignKey("dbo.BusinessInfoes", t => t.OrganizationId, cascadeDelete: false)
                .ForeignKey("dbo.Reps", t => t.Rep_id)
                .Index(t => t.FocalPointId)
                .Index(t => t.OrganizationId)
                .Index(t => t.Rep_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "Rep_id", "dbo.Reps");
            DropForeignKey("dbo.Members", "OrganizationId", "dbo.BusinessInfoes");
            DropForeignKey("dbo.Members", "FocalPointId", "dbo.People");
            DropForeignKey("dbo.AdInfoes", "MemberID", "dbo.Members");
            DropForeignKey("dbo.Institutions", "Rep_id", "dbo.Reps");
            DropForeignKey("dbo.Institutions", "OrganizationId", "dbo.BusinessInfoes");
            DropForeignKey("dbo.BusinessInfoContactInfoes", "BusinessInfoId", "dbo.BusinessInfoes");
            DropForeignKey("dbo.Institutions", "FocalPointId", "dbo.People");
            DropForeignKey("dbo.AdInfoes", "InstitutionID", "dbo.Institutions");
            DropForeignKey("dbo.Reps", "IdentityId", "dbo.People");
            DropForeignKey("dbo.Admins", "Rep_id", "dbo.Reps");
            DropForeignKey("dbo.Admins", "IdentityId", "dbo.People");
            DropForeignKey("dbo.PersonContactInfoes", "Person_id", "dbo.People");
            DropIndex("dbo.Members", new[] { "Rep_id" });
            DropIndex("dbo.Members", new[] { "OrganizationId" });
            DropIndex("dbo.Members", new[] { "FocalPointId" });
            DropIndex("dbo.BusinessInfoContactInfoes", new[] { "BusinessInfoId" });
            DropIndex("dbo.AdInfoes", new[] { "MemberID" });
            DropIndex("dbo.AdInfoes", new[] { "InstitutionID" });
            DropIndex("dbo.Institutions", new[] { "Rep_id" });
            DropIndex("dbo.Institutions", new[] { "OrganizationId" });
            DropIndex("dbo.Institutions", new[] { "FocalPointId" });
            DropIndex("dbo.PersonContactInfoes", new[] { "Person_id" });
            DropIndex("dbo.Admins", new[] { "Rep_id" });
            DropIndex("dbo.Admins", new[] { "IdentityId" });
            DropIndex("dbo.Reps", new[] { "IdentityId" });
            DropTable("dbo.Members");
            DropTable("dbo.BusinessInfoContactInfoes");
            DropTable("dbo.BusinessInfoes");
            DropTable("dbo.AdInfoes");
            DropTable("dbo.Institutions");
            DropTable("dbo.PersonContactInfoes");
            DropTable("dbo.People");
            DropTable("dbo.Admins");
            DropTable("dbo.Reps");
        }
    }
}
