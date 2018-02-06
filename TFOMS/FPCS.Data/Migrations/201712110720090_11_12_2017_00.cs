namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11_12_2017_00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.worker",
                c => new
                    {
                        worker_id = c.Long(nullable: false, identity: true),
                        surname = c.String(nullable: false, maxLength: 100),
                        name = c.String(nullable: false, maxLength: 100),
                        secondname = c.String(maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTime(nullable: false),
                        updateddate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.worker_id);
            
            AddColumn("dbo.handappeal", "WorkerId", c => c.Long());
            CreateIndex("dbo.handappeal", "WorkerId");
            AddForeignKey("dbo.handappeal", "WorkerId", "dbo.worker", "worker_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.handappeal", "WorkerId", "dbo.worker");
            DropIndex("dbo.handappeal", new[] { "WorkerId" });
            DropColumn("dbo.handappeal", "WorkerId");
            DropTable("dbo.worker");
        }
    }
}
