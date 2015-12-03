namespace AgilAds.MigrationsAgilAdsDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BusinessInfoId = c.Int(nullable: false),
                        Fullname = c.String(),
                        Firstname = c.String(nullable: false, maxLength: 25),
                        Lastname = c.String(nullable: false, maxLength: 25),
                        Username = c.String(maxLength: 25),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusinessInfoes", t => t.BusinessInfoId, cascadeDelete: true)
                .Index(t => t.BusinessInfoId);
            
            CreateTable(
                "dbo.BusinessInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        OrganizationName = c.String(nullable: false, maxLength: 35),
                        BankAcctNo = c.String(maxLength: 2000),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 25),
                        ArcSum = c.Binary(),
                        Secret = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.BusinessContacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BusinessInfoId = c.Int(nullable: false),
                        Method = c.Int(nullable: false),
                        Contact = c.String(nullable: false, maxLength: 2000),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusinessInfoes", t => t.BusinessInfoId, cascadeDelete: true)
                .Index(t => t.BusinessInfoId);
            
            CreateTable(
                "dbo.AdInfoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        InstitutionID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Expiration = c.DateTime(nullable: false),
                        DynamicResourceURL = c.String(maxLength: 2000),
                        ReqStatus = c.Int(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Institutions", t => t.InstitutionID)
                .ForeignKey("dbo.Members", t => t.MemberID)
                .Index(t => t.InstitutionID)
                .Index(t => t.MemberID);
            
            CreateTable(
                "dbo.InstitutionPayments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        To = c.Int(nullable: false),
                        From = c.Int(nullable: false),
                        AccountNo = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Institutions", t => t.To)
                .ForeignKey("dbo.Members", t => t.From)
                .Index(t => t.To)
                .Index(t => t.From);
            
            CreateTable(
                "dbo.RepPayments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        To = c.Int(nullable: false),
                        From = c.Int(nullable: false),
                        AccountNo = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Members", t => t.From)
                .ForeignKey("dbo.Reps", t => t.To)
                .Index(t => t.To)
                .Index(t => t.From);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        Expiration = c.DateTime(),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(maxLength: 25),
                        Rep_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.People", t => t.IdentityId, cascadeDelete: true)
                .ForeignKey("dbo.Reps", t => t.Rep_id)
                .Index(t => t.IdentityId)
                .Index(t => t.Rep_id);
            
            CreateTable(
                "dbo.PersonalContacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        Method = c.Int(nullable: false),
                        Contact = c.String(nullable: false, maxLength: 2000),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.People", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.Privs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 25),
                        Context = c.String(nullable: false, maxLength: 25),
                        Action = c.String(nullable: false, maxLength: 25),
                        Modified = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Reps",
                c => new
                    {
                        id = c.Int(nullable: false),
                        Region = c.String(maxLength: 25),
                        FocalPointId = c.Int(nullable: false),
                        Fee = c.Double(nullable: false),
                        TaxRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusinessInfoes", t => t.id)
                .ForeignKey("dbo.People", t => t.FocalPointId, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => t.FocalPointId);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        id = c.Int(nullable: false),
                        Rep_id = c.Int(),
                        FocalPointId = c.Int(nullable: false),
                        MonthlyAdFee = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusinessInfoes", t => t.id)
                .ForeignKey("dbo.Reps", t => t.Rep_id)
                .ForeignKey("dbo.People", t => t.FocalPointId, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => t.Rep_id)
                .Index(t => t.FocalPointId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        id = c.Int(nullable: false),
                        Rep_id = c.Int(),
                        FocalPointId = c.Int(nullable: false),
                        StaticMsg = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusinessInfoes", t => t.id)
                .ForeignKey("dbo.Reps", t => t.Rep_id)
                .ForeignKey("dbo.People", t => t.FocalPointId, cascadeDelete: true)
                .Index(t => t.id)
                .Index(t => t.Rep_id)
                .Index(t => t.FocalPointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "FocalPointId", "dbo.People");
            DropForeignKey("dbo.Members", "Rep_id", "dbo.Reps");
            DropForeignKey("dbo.Members", "id", "dbo.BusinessInfoes");
            DropForeignKey("dbo.Institutions", "FocalPointId", "dbo.People");
            DropForeignKey("dbo.Institutions", "Rep_id", "dbo.Reps");
            DropForeignKey("dbo.Institutions", "id", "dbo.BusinessInfoes");
            DropForeignKey("dbo.Reps", "FocalPointId", "dbo.People");
            DropForeignKey("dbo.Reps", "id", "dbo.BusinessInfoes");
            DropForeignKey("dbo.PersonalContacts", "IdentityId", "dbo.People");
            DropForeignKey("dbo.RepPayments", "To", "dbo.Reps");
            DropForeignKey("dbo.Admins", "Rep_id", "dbo.Reps");
            DropForeignKey("dbo.Admins", "IdentityId", "dbo.People");
            DropForeignKey("dbo.RepPayments", "From", "dbo.Members");
            DropForeignKey("dbo.InstitutionPayments", "From", "dbo.Members");
            DropForeignKey("dbo.InstitutionPayments", "To", "dbo.Institutions");
            DropForeignKey("dbo.AdInfoes", "MemberID", "dbo.Members");
            DropForeignKey("dbo.AdInfoes", "InstitutionID", "dbo.Institutions");
            DropForeignKey("dbo.People", "BusinessInfoId", "dbo.BusinessInfoes");
            DropForeignKey("dbo.BusinessContacts", "BusinessInfoId", "dbo.BusinessInfoes");
            DropIndex("dbo.Members", new[] { "FocalPointId" });
            DropIndex("dbo.Members", new[] { "Rep_id" });
            DropIndex("dbo.Members", new[] { "id" });
            DropIndex("dbo.Institutions", new[] { "FocalPointId" });
            DropIndex("dbo.Institutions", new[] { "Rep_id" });
            DropIndex("dbo.Institutions", new[] { "id" });
            DropIndex("dbo.Reps", new[] { "FocalPointId" });
            DropIndex("dbo.Reps", new[] { "id" });
            DropIndex("dbo.PersonalContacts", new[] { "IdentityId" });
            DropIndex("dbo.Admins", new[] { "Rep_id" });
            DropIndex("dbo.Admins", new[] { "IdentityId" });
            DropIndex("dbo.RepPayments", new[] { "From" });
            DropIndex("dbo.RepPayments", new[] { "To" });
            DropIndex("dbo.InstitutionPayments", new[] { "From" });
            DropIndex("dbo.InstitutionPayments", new[] { "To" });
            DropIndex("dbo.AdInfoes", new[] { "MemberID" });
            DropIndex("dbo.AdInfoes", new[] { "InstitutionID" });
            DropIndex("dbo.BusinessContacts", new[] { "BusinessInfoId" });
            DropIndex("dbo.People", new[] { "BusinessInfoId" });
            DropTable("dbo.Members");
            DropTable("dbo.Institutions");
            DropTable("dbo.Reps");
            DropTable("dbo.Privs");
            DropTable("dbo.PersonalContacts");
            DropTable("dbo.Admins");
            DropTable("dbo.RepPayments");
            DropTable("dbo.InstitutionPayments");
            DropTable("dbo.AdInfoes");
            DropTable("dbo.BusinessContacts");
            DropTable("dbo.BusinessInfoes");
            DropTable("dbo.People");
        }
    }
}
