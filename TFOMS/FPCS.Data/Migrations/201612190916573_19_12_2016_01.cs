namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_12_2016_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.journalappeal", "smoid", c => c.Long());
            CreateIndex("dbo.journalappeal", "smoid");
            AddForeignKey("dbo.journalappeal", "smoid", "dbo.smo", "smoid");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.journalappeal", "smoid", "dbo.smo");
            DropIndex("dbo.journalappeal", new[] { "smoid" });
            DropColumn("dbo.journalappeal", "smoid");
        }
    }
}
