namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017121801 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.medical_organization",
                c => new
                    {
                        mo_id = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTime(nullable: false),
                        updateddate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.mo_id);
            
            CreateTable(
                "dbo.organizations",
                c => new
                    {
                        organization_id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTime(nullable: false),
                        updateddate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.organization_id);
            
            AddColumn("dbo.handappeal", "mo_id", c => c.Long());
            AddColumn("dbo.handappeal", "organization_id", c => c.Long());
            CreateIndex("dbo.handappeal", "mo_id");
            CreateIndex("dbo.handappeal", "organization_id");
            AddForeignKey("dbo.handappeal", "mo_id", "dbo.medical_organization", "mo_id");
            AddForeignKey("dbo.handappeal", "organization_id", "dbo.organizations", "organization_id");
            DropColumn("dbo.handappeal", "medical_organization_name");
            DropColumn("dbo.handappeal", "guide_medical_organization_name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "guide_medical_organization_name", c => c.String());
            AddColumn("dbo.handappeal", "medical_organization_name", c => c.String());
            DropForeignKey("dbo.handappeal", "organization_id", "dbo.organizations");
            DropForeignKey("dbo.handappeal", "mo_id", "dbo.medical_organization");
            DropIndex("dbo.handappeal", new[] { "organization_id" });
            DropIndex("dbo.handappeal", new[] { "mo_id" });
            DropColumn("dbo.handappeal", "organization_id");
            DropColumn("dbo.handappeal", "mo_id");
            DropTable("dbo.organizations");
            DropTable("dbo.medical_organization");
        }
    }
}
