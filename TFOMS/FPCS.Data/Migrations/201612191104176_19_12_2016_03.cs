namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19_12_2016_03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.flk", "rnsmo", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.flk", "rnsmo", c => c.String(maxLength: 3));
        }
    }
}
