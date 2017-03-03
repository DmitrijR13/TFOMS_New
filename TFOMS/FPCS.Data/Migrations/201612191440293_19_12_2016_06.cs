namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_12_2016_06 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.journalappeal", new[] { "appealtheme" });
            AlterColumn("dbo.journalappeal", "appealtheme", c => c.Long(nullable: false));
            CreateIndex("dbo.journalappeal", "appealtheme");
        }
        
        public override void Down()
        {
            DropIndex("dbo.journalappeal", new[] { "appealtheme" });
            AlterColumn("dbo.journalappeal", "appealtheme", c => c.Long());
            CreateIndex("dbo.journalappeal", "appealtheme");
        }
    }
}
