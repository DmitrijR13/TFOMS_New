namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07_12_2016_00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.flk",
                c => new
                    {
                        flkid = c.Long(nullable: false, identity: true),
                        basefilename = c.String(nullable: false),
                        errorcode = c.String(nullable: false),
                        fieldname = c.String(),
                        baseentitiyname = c.String(),
                        appealnumber = c.String(),
                        comment = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                        createddate = c.DateTime(nullable: false, precision: 7),
                        updateddate = c.DateTime(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.flkid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.flk");
        }
    }
}
