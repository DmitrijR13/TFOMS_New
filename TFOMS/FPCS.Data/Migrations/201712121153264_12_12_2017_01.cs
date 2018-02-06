namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12_12_2017_01 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.handappeal", name: "AppealFile", newName: "appeal_file");
            AddColumn("dbo.handappeal", "appeal_file_name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.handappeal", "appeal_file_name");
            RenameColumn(table: "dbo.handappeal", name: "appeal_file", newName: "AppealFile");
        }
    }
}
