namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_12_2016_04 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.journalappeal", "appealtheme");
        }
        
        public override void Down()
        {
            AddColumn("dbo.journalappeal", "appealtheme", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
