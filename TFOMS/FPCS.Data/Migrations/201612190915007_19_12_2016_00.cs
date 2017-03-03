namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_12_2016_00 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.journalappeal", "appealorganizationcode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.journalappeal", "appealorganizationcode", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
