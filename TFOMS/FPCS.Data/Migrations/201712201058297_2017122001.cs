namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017122001 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.journalappeal", new[] { "sourceincomeid" });
            DropIndex("dbo.journalappeal", new[] { "typeofaddressingid" });
            DropIndex("dbo.journalappeal", new[] { "wayofaddressingid" });
            DropIndex("dbo.journalappeal", new[] { "appealtheme" });
            DropIndex("dbo.journalappeal", new[] { "appealorganizationid" });
            DropIndex("dbo.journalappeal", new[] { "takingappeallineid" });
            AlterColumn("dbo.journalappeal", "appealuniquenumber", c => c.String(maxLength: 36));
            AlterColumn("dbo.journalappeal", "date", c => c.DateTime());
            AlterColumn("dbo.journalappeal", "sourceincomeid", c => c.Long());
            AlterColumn("dbo.journalappeal", "typeofaddressingid", c => c.Long());
            AlterColumn("dbo.journalappeal", "wayofaddressingid", c => c.Long());
            AlterColumn("dbo.journalappeal", "appealtheme", c => c.Long());
            AlterColumn("dbo.journalappeal", "appealorganizationid", c => c.Long());
            AlterColumn("dbo.journalappeal", "acceptedby", c => c.String());
            AlterColumn("dbo.journalappeal", "takingappeallineid", c => c.Long());
            AlterColumn("dbo.journalappeal", "appealorganizationcode", c => c.String());
            AlterColumn("dbo.journalappeal", "appealplanenddate", c => c.DateTime());
            CreateIndex("dbo.journalappeal", "sourceincomeid");
            CreateIndex("dbo.journalappeal", "typeofaddressingid");
            CreateIndex("dbo.journalappeal", "wayofaddressingid");
            CreateIndex("dbo.journalappeal", "appealtheme");
            CreateIndex("dbo.journalappeal", "appealorganizationid");
            CreateIndex("dbo.journalappeal", "takingappeallineid");
        }
        
        public override void Down()
        {
            DropIndex("dbo.journalappeal", new[] { "takingappeallineid" });
            DropIndex("dbo.journalappeal", new[] { "appealorganizationid" });
            DropIndex("dbo.journalappeal", new[] { "appealtheme" });
            DropIndex("dbo.journalappeal", new[] { "wayofaddressingid" });
            DropIndex("dbo.journalappeal", new[] { "typeofaddressingid" });
            DropIndex("dbo.journalappeal", new[] { "sourceincomeid" });
            AlterColumn("dbo.journalappeal", "appealplanenddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "appealorganizationcode", c => c.String(nullable: false));
            AlterColumn("dbo.journalappeal", "takingappeallineid", c => c.Long(nullable: false));
            AlterColumn("dbo.journalappeal", "acceptedby", c => c.String(nullable: false));
            AlterColumn("dbo.journalappeal", "appealorganizationid", c => c.Long(nullable: false));
            AlterColumn("dbo.journalappeal", "appealtheme", c => c.Long(nullable: false));
            AlterColumn("dbo.journalappeal", "wayofaddressingid", c => c.Long(nullable: false));
            AlterColumn("dbo.journalappeal", "typeofaddressingid", c => c.Long(nullable: false));
            AlterColumn("dbo.journalappeal", "sourceincomeid", c => c.Long(nullable: false));
            AlterColumn("dbo.journalappeal", "date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "appealuniquenumber", c => c.String(nullable: false, maxLength: 36));
            CreateIndex("dbo.journalappeal", "takingappeallineid");
            CreateIndex("dbo.journalappeal", "appealorganizationid");
            CreateIndex("dbo.journalappeal", "appealtheme");
            CreateIndex("dbo.journalappeal", "wayofaddressingid");
            CreateIndex("dbo.journalappeal", "typeofaddressingid");
            CreateIndex("dbo.journalappeal", "sourceincomeid");
        }
    }
}
