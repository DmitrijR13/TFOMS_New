namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017121800 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.worker", "is_head", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.worker", "is_head");
        }
    }
}
