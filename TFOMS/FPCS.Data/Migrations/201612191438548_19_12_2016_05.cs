namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_12_2016_05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.journalappeal", "appealtheme", c => c.Long());
            CreateIndex("dbo.journalappeal", "appealtheme");
            AddForeignKey("dbo.journalappeal", "appealtheme", "dbo.themeappealcitizens", "themeappealcitizensid");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.journalappeal", "appealtheme", "dbo.themeappealcitizens");
            DropIndex("dbo.journalappeal", new[] { "appealtheme" });
            DropColumn("dbo.journalappeal", "appealtheme");
        }
    }
}
