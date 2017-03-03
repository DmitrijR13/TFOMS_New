namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28_11_2016_00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.sourceincome",
                c => new
                    {
                        sourceincomeid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.sourceincomeid);
            
            CreateTable(
                "dbo.journalappeal",
                c => new
                    {
                        journalappealid = c.Long(nullable: false, identity: true),
                        appealuniquenumber = c.String(nullable: false, maxLength: 36),
                        date = c.DateTimeOffset(nullable: false, precision: 7),
                        time_ = c.String(),
                        sourceincomeid = c.Long(nullable: false),
                        organizationname = c.String(),
                        typeofaddressingid = c.Long(nullable: false),
                        wayofaddressingid = c.Long(nullable: false),
                        appealtheme = c.String(nullable: false, maxLength: 500),
                        appealcontent = c.String(maxLength: 5000),
                        complaintid = c.Long(),
                        appealorganizationid = c.Long(nullable: false),
                        appealorganizationcode = c.String(nullable: false, maxLength: 10),
                        takingappeallineid = c.Long(nullable: false),
                        acceptedby = c.String(nullable: false),
                        reviewappeallineid = c.Long(),
                        responsible = c.String(nullable: false),
                        appealplanenddate = c.DateTimeOffset(nullable: false, precision: 7),
                        appealfactenddate = c.DateTimeOffset(precision: 7),
                        appealresultid = c.Long(),
                        applicantsurname = c.String(),
                        applicantname = c.String(),
                        applicantsecondname = c.String(),
                        applicantbirthdate = c.DateTimeOffset(precision: 7),
                        applicantenp = c.String(),
                        applicantsmo = c.String(),
                        applicanttypedocument = c.String(),
                        applicantdocumentseries = c.String(),
                        applicantdocumentnumber = c.String(),
                        applicantfeedbackaddress = c.String(),
                        applicantphonenumber = c.String(),
                        applicantemail = c.String(),
                        receivedtreatmentpersonsurname = c.String(),
                        receivedtreatmentpersonname = c.String(),
                        receivedtreatmentpersonsecondname = c.String(),
                        receivedtreatmentpersonbirthdate = c.DateTimeOffset(precision: 7),
                        receivedtreatmentpersonenp = c.String(),
                        receivedtreatmentpersonsmo = c.String(),
                        receivedtreatmentpersonypedocument = c.String(),
                        receivedtreatmentpersondocumentseries = c.String(),
                        receivedtreatmentpersondocumentnumber = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.journalappealid)
                .ForeignKey("dbo.appealorganization", t => t.appealorganizationid)
                .ForeignKey("dbo.appealresult", t => t.appealresultid)
                .ForeignKey("dbo.complaint", t => t.complaintid)
                .ForeignKey("dbo.reviewtreatmentline", t => t.reviewappeallineid)
                .ForeignKey("dbo.sourceincome", t => t.sourceincomeid)
                .ForeignKey("dbo.takingtreatmentline", t => t.takingappeallineid)
                .ForeignKey("dbo.typeofaddressing", t => t.typeofaddressingid)
                .ForeignKey("dbo.wayofaddressing", t => t.wayofaddressingid)
                .Index(t => t.sourceincomeid)
                .Index(t => t.typeofaddressingid)
                .Index(t => t.wayofaddressingid)
                .Index(t => t.complaintid)
                .Index(t => t.appealorganizationid)
                .Index(t => t.takingappeallineid)
                .Index(t => t.reviewappeallineid)
                .Index(t => t.appealresultid);
            
            CreateTable(
                "dbo.appealorganization",
                c => new
                    {
                        appealorganizationid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.appealorganizationid);
            
            CreateTable(
                "dbo.appealresult",
                c => new
                    {
                        appealresultid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.appealresultid);
            
            CreateTable(
                "dbo.complaint",
                c => new
                    {
                        complaintid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.complaintid);
            
            CreateTable(
                "dbo.reviewtreatmentline",
                c => new
                    {
                        reviewappeallineid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.reviewappeallineid);
            
            CreateTable(
                "dbo.takingtreatmentline",
                c => new
                    {
                        takingappeallineid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.takingappeallineid);
            
            CreateTable(
                "dbo.typeofaddressing",
                c => new
                    {
                        typeofaddressingid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.typeofaddressingid);
            
            CreateTable(
                "dbo.wayofaddressing",
                c => new
                    {
                        wayofaddressingid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 3),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.wayofaddressingid);
            
            CreateTable(
                "dbo.themeappealcitizens",
                c => new
                    {
                        themeappealcitizensid = c.Long(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 8),
                        name = c.String(nullable: false, maxLength: 100),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.themeappealcitizensid);
            
            CreateTable(
                "dbo.admin",
                c => new
                    {
                        dbuserid = c.Long(nullable: false, identity: true),
                        unqdbuserid = c.Long(nullable: false),
                        login = c.String(nullable: false, maxLength: 100),
                        password = c.String(nullable: false, maxLength: 100),
                        email = c.String(maxLength: 100),
                        firstname = c.String(nullable: false, maxLength: 100),
                        lastname = c.String(nullable: false, maxLength: 100),
                        fullname = c.String(nullable: false, maxLength: 200),
                        middleinitial = c.String(maxLength: 50),
                        islocked = c.Boolean(nullable: false),
                        role = c.Int(nullable: false),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTimeOffset(nullable: false, precision: 7),
                        updateddate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.dbuserid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.journalappeal", "wayofaddressingid", "dbo.wayofaddressing");
            DropForeignKey("dbo.journalappeal", "typeofaddressingid", "dbo.typeofaddressing");
            DropForeignKey("dbo.journalappeal", "takingappeallineid", "dbo.takingtreatmentline");
            DropForeignKey("dbo.journalappeal", "sourceincomeid", "dbo.sourceincome");
            DropForeignKey("dbo.journalappeal", "reviewappeallineid", "dbo.reviewtreatmentline");
            DropForeignKey("dbo.journalappeal", "complaintid", "dbo.complaint");
            DropForeignKey("dbo.journalappeal", "appealresultid", "dbo.appealresult");
            DropForeignKey("dbo.journalappeal", "appealorganizationid", "dbo.appealorganization");
            DropIndex("dbo.journalappeal", new[] { "appealresultid" });
            DropIndex("dbo.journalappeal", new[] { "reviewappeallineid" });
            DropIndex("dbo.journalappeal", new[] { "takingappeallineid" });
            DropIndex("dbo.journalappeal", new[] { "appealorganizationid" });
            DropIndex("dbo.journalappeal", new[] { "complaintid" });
            DropIndex("dbo.journalappeal", new[] { "wayofaddressingid" });
            DropIndex("dbo.journalappeal", new[] { "typeofaddressingid" });
            DropIndex("dbo.journalappeal", new[] { "sourceincomeid" });
            DropTable("dbo.admin");
            DropTable("dbo.themeappealcitizens");
            DropTable("dbo.wayofaddressing");
            DropTable("dbo.typeofaddressing");
            DropTable("dbo.takingtreatmentline");
            DropTable("dbo.reviewtreatmentline");
            DropTable("dbo.complaint");
            DropTable("dbo.appealresult");
            DropTable("dbo.appealorganization");
            DropTable("dbo.journalappeal");
            DropTable("dbo.sourceincome");
        }
    }
}
