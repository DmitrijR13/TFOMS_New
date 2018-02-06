namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07_12_2017_00 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.handappeal", "unique_number_part_1", c => c.Int(nullable: false));
            AddColumn("dbo.handappeal", "unique_number_part_2", c => c.Int(nullable: false));
            AddColumn("dbo.handappeal", "unique_number_part_3", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.handappeal", "unique_number_part_3");
            DropColumn("dbo.handappeal", "unique_number_part_2");
            DropColumn("dbo.handappeal", "unique_number_part_1");
        }
    }
}
