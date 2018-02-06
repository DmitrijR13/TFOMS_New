namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28_11_2017_00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.handappeal",
                c => new
                    {
                        journalappealid = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.journalappealid)
                .ForeignKey("dbo.journalappeal", t => t.journalappealid)
                .Index(t => t.journalappealid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.handappeal", "journalappealid", "dbo.journalappeal");
            DropIndex("dbo.handappeal", new[] { "journalappealid" });
            DropTable("dbo.handappeal");
        }
    }
}
