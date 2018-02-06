namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017121900 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.handappeal", "med_orgs", c => c.String());
            DropColumn("dbo.handappeal", "ded_orgs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "ded_orgs", c => c.String());
            DropColumn("dbo.handappeal", "med_orgs");
        }
    }
}