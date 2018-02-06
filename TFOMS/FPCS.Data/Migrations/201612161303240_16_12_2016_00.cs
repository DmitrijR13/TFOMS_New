namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16_12_2016_00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.smo",
                c => new
                    {
                        smoid = c.Long(nullable: false, identity: true),
                        smocode = c.String(nullable: false, maxLength: 10),
                        kpp = c.String(nullable: false, maxLength: 9),
                        fullname = c.String(nullable: false, maxLength: 100),
                        shortname = c.String(nullable: false, maxLength: 100),
                        factaddress = c.String(nullable: false, maxLength: 150),
                        director = c.String(nullable: false, maxLength: 150),
                        filialdirector = c.String(nullable: false, maxLength: 150),
                        licenseinfo = c.String(maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.smoid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.smo");
        }
    }
}
