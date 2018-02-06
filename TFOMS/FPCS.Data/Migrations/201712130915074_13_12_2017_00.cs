namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13_12_2017_00 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.handappeal", "inspection_tfoms_worker");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "inspection_tfoms_worker", c => c.String());
        }
    }
}
