namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21_12_2016_02 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.journalappeal", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.journalappeal", "test", c => c.String());
        }
    }
}
