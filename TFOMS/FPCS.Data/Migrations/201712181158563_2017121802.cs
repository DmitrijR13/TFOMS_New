namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017121802 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.medical_organization", "code", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.medical_organization", "code", c => c.String(nullable: false, maxLength: 3));
        }
    }
}
