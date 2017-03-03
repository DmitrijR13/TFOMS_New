namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28_11_2016_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.complaint", "code", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.complaint", "code", c => c.String(nullable: false, maxLength: 3));
        }
    }
}
