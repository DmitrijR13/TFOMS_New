namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16_12_2016_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.smo", "filialdirector", c => c.String(maxLength: 150));
            AlterColumn("dbo.smo", "licenseinfo", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.smo", "licenseinfo", c => c.String(maxLength: 100));
            AlterColumn("dbo.smo", "filialdirector", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
