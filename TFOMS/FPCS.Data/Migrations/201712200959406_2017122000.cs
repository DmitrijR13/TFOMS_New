namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017122000 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.handappeal", "unique_number_part_2");
            DropColumn("dbo.handappeal", "unique_number_part_3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "unique_number_part_3", c => c.Int(nullable: false));
            AddColumn("dbo.handappeal", "unique_number_part_2", c => c.Int(nullable: false));
        }
    }
}
