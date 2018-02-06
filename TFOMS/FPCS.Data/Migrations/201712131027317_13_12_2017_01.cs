namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13_12_2017_01 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.handappeal", name: "WorkerId", newName: "worker_id");
            DropIndex("dbo.handappeal", new[] { "IX_WorkerId" });
            CreateIndex("dbo.handappeal", "worker_id");
            CreateTable(
                "dbo.passed_event",
                c => new
                    {
                        passed_event_id = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTime(nullable: false),
                        updateddate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.passed_event_id);
            
            AddColumn("dbo.handappeal", "passed_event_id", c => c.Long());
            CreateIndex("dbo.handappeal", "passed_event_id");
            AddForeignKey("dbo.handappeal", "passed_event_id", "dbo.passed_event", "passed_event_id");
            DropColumn("dbo.handappeal", "inspection_work");
        }
        
        public override void Down()
        {
            AddColumn("dbo.handappeal", "inspection_work", c => c.String());
            DropForeignKey("dbo.handappeal", "passed_event_id", "dbo.passed_event");
            DropIndex("dbo.handappeal", new[] { "passed_event_id" });
            DropColumn("dbo.handappeal", "passed_event_id");
            DropTable("dbo.passed_event");
            RenameIndex(table: "dbo.handappeal", name: "IX_worker_id", newName: "IX_WorkerId");
            DropIndex("dbo.handappeal", new[] { "IX_worker_id" });
            CreateIndex("dbo.handappeal", "IX_WorkerId");
            RenameColumn(table: "dbo.handappeal", name: "worker_id", newName: "WorkerId");
        }
    }
}
