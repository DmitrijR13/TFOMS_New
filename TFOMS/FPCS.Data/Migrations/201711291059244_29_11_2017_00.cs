namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29_11_2017_00 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.admin", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.admin", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("users.user", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("users.user", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.smo", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.smo", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "appealplanenddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "appealfactenddate", c => c.DateTime());
            AlterColumn("dbo.journalappeal", "applicantbirthdate", c => c.DateTime());
            AlterColumn("dbo.journalappeal", "receivedtreatmentpersonbirthdate", c => c.DateTime());
            AlterColumn("dbo.journalappeal", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.journalappeal", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealorganization", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealorganization", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealresult", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.appealresult", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.complaint", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.complaint", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.reviewtreatmentline", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.reviewtreatmentline", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.sourceincome", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.sourceincome", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.takingtreatmentline", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.takingtreatmentline", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.themeappealcitizens", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.themeappealcitizens", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.typeofaddressing", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.typeofaddressing", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.wayofaddressing", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.wayofaddressing", "updateddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.flk", "createddate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.flk", "updateddate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.flk", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.flk", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.wayofaddressing", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.wayofaddressing", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.typeofaddressing", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.typeofaddressing", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.themeappealcitizens", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.themeappealcitizens", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.takingtreatmentline", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.takingtreatmentline", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.sourceincome", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.sourceincome", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.reviewtreatmentline", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.reviewtreatmentline", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.complaint", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.complaint", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.appealresult", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.appealresult", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.appealorganization", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.appealorganization", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.journalappeal", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.journalappeal", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.journalappeal", "receivedtreatmentpersonbirthdate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.journalappeal", "applicantbirthdate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.journalappeal", "appealfactenddate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.journalappeal", "appealplanenddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.journalappeal", "date", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.smo", "UpdatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.smo", "CreatedDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("users.user", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("users.user", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.admin", "updateddate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.admin", "createddate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
