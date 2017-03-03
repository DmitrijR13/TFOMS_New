namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017030100 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.journalappeal", "responsible", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.journalappeal", "responsible", c => c.String(nullable: false));
        }
    }
}
