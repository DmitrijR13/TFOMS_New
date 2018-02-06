namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017121803 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.handappeal", "mo_id", "dbo.medical_organization");
            DropForeignKey("dbo.handappeal", "organization_id", "dbo.organizations");
            DropIndex("dbo.handappeal", new[] { "mo_id" });
            DropIndex("dbo.handappeal", new[] { "organization_id" });
            AddColumn("dbo.handappeal", "ded_orgs", c => c.String());
            AddColumn("dbo.handappeal", "organizations", c => c.String());
            DropColumn("dbo.handappeal", "mo_id");
            DropColumn("dbo.handappeal", "organization_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "organization_id", c => c.Long());
            AddColumn("dbo.handappeal", "mo_id", c => c.Long());
            DropColumn("dbo.handappeal", "organizations");
            DropColumn("dbo.handappeal", "ded_orgs");
            CreateIndex("dbo.handappeal", "organization_id");
            CreateIndex("dbo.handappeal", "mo_id");
            AddForeignKey("dbo.handappeal", "organization_id", "dbo.organizations", "organization_id");
            AddForeignKey("dbo.handappeal", "mo_id", "dbo.medical_organization", "mo_id");
        }
    }
}
