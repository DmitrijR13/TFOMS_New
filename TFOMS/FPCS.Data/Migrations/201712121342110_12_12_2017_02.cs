namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12_12_2017_02 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.handappeal", "justified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "justified", c => c.Int(nullable: false));
        }
    }
}
