namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15_12_2016_00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.user",
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
                        createddate = c.DateTime(nullable: false, precision: 7),
                        updateddate = c.DateTime(nullable: false, precision: 7),
                        smoregnum = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.dbuserid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.user");
        }
    }
}
