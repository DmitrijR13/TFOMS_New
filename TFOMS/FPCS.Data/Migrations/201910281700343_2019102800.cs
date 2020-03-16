namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2019102800 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.themeappealcitizens", "dateclose", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.themeappealcitizens", "dateclose");
        }
    }
}
