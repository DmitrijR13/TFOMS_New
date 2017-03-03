namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14_12_2016_00 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.flk", "rnsmo", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.flk", "rnsmo");
        }
    }
}
