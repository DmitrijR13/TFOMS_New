namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21_12_2016_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.journalappeal", "test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.journalappeal", "test");
        }
    }
}
