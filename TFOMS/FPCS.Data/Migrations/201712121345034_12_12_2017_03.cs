namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12_12_2017_03 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.handappeal", "appeal_code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "appeal_code", c => c.String());
        }
    }
}
