namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_12_2016_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.journalappeal", "appealorganizationcode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.journalappeal", "appealorganizationcode");
        }
    }
}
