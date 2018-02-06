namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017122100 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.handappeal", "answer_file", c => c.Binary());
            AddColumn("dbo.handappeal", "answer_file_name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.handappeal", "answer_file_name");
            DropColumn("dbo.handappeal", "answer_file");
        }
    }
}
