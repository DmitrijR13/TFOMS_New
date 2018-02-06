namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017122101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.smo", "head_surname", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.smo", "head_name", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.smo", "head_second_name", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.smo", "head_position", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.worker", "phone", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.worker", "phone");
            DropColumn("dbo.smo", "head_position");
            DropColumn("dbo.smo", "head_second_name");
            DropColumn("dbo.smo", "head_name");
            DropColumn("dbo.smo", "head_surname");
        }
    }
}
