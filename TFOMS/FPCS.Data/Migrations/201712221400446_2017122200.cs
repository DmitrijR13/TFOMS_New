namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017122200 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.admin", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.admin", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("users.user", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("users.user", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.smo", "CreatedDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.smo", "UpdatedDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.journalappeal", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.journalappeal", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.appealorganization", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.appealorganization", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.appealresult", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.appealresult", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.complaint", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.complaint", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.reviewtreatmentline", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.reviewtreatmentline", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.sourceincome", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.sourceincome", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.takingtreatmentline", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.takingtreatmentline", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.themeappealcitizens", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.themeappealcitizens", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.typeofaddressing", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.typeofaddressing", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.wayofaddressing", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.wayofaddressing", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.passed_event", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.passed_event", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.worker", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.worker", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.flk", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.flk", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.medical_organization", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.medical_organization", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.organizations", "createddate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.organizations", "updateddate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.organizations", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.organizations", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.medical_organization", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.medical_organization", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.flk", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.flk", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.worker", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.worker", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.passed_event", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.passed_event", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.wayofaddressing", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.wayofaddressing", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.typeofaddressing", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.typeofaddressing", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.themeappealcitizens", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.themeappealcitizens", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.takingtreatmentline", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.takingtreatmentline", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.sourceincome", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.sourceincome", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.reviewtreatmentline", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.reviewtreatmentline", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.complaint", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.complaint", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealresult", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealresult", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealorganization", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealorganization", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.smo", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.smo", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("users.user", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("users.user", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.admin", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.admin", "createddate", c => c.DateTime(nullable: false));
        }
    }
}
