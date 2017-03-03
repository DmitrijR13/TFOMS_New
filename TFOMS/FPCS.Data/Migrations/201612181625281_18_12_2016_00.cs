namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18_12_2016_00 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.user", "smoid", c => c.Long());
            CreateIndex("dbo.user", "smoid");
            AddForeignKey("dbo.user", "smoid", "dbo.smo", "smoid");
            DropColumn("dbo.user", "smoregnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.user", "smoregnum", c => c.String(maxLength: 3));
            DropForeignKey("dbo.user", "smoid", "dbo.smo");
            DropIndex("dbo.user", new[] { "smoid" });
            DropColumn("dbo.user", "smoid");
        }
    }
}
