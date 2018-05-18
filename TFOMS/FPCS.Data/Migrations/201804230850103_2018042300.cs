namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2018042300 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.typeofaddressing", "is_update_date_end", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.typeofaddressing", "is_update_date_end");
        }
    }
}
