namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12_12_2017_00 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.handappeal", "AppealFile", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.handappeal", "AppealFile");
        }
    }
}
