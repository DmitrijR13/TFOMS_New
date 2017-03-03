namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21_12_2016_00 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.user", newSchema: "users");
        }
        
        public override void Down()
        {
            MoveTable(name: "users.user", newSchema: "dbo");
        }
    }
}
