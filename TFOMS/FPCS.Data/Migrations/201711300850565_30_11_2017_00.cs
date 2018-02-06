namespace FPCS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30_11_2017_00 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.handappeal", "ozpz_registration_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.handappeal", "appeal_number", c => c.String());
            AddColumn("dbo.handappeal", "medical_organization_name", c => c.String());
            AddColumn("dbo.handappeal", "guide_number", c => c.String());
            AddColumn("dbo.handappeal", "guide_medical_organization_name", c => c.String());
            AddColumn("dbo.handappeal", "guide_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.handappeal", "received_feedback_address", c => c.String());
            AddColumn("dbo.handappeal", "received_phone_number", c => c.String());
            AddColumn("dbo.handappeal", "inspection_tfoms_worker", c => c.String());
            AddColumn("dbo.handappeal", "inspection_work", c => c.String());
            AddColumn("dbo.handappeal", "inspection_kind", c => c.String());
            AddColumn("dbo.handappeal", "inspection_expert", c => c.String());
            AddColumn("dbo.handappeal", "review_appeal_number", c => c.String());
            AddColumn("dbo.handappeal", "review_appeal_code", c => c.String());
            AddColumn("dbo.handappeal", "review_appeal_penalty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.handappeal", "review_appeal_cashback", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.handappeal", "justified", c => c.Int(nullable: false));
            AddColumn("dbo.handappeal", "appeal_code", c => c.String());
            AddColumn("dbo.handappeal", "comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.handappeal", "comment");
            DropColumn("dbo.handappeal", "appeal_code");
            DropColumn("dbo.handappeal", "justified");
            DropColumn("dbo.handappeal", "review_appeal_cashback");
            DropColumn("dbo.handappeal", "review_appeal_penalty");
            DropColumn("dbo.handappeal", "review_appeal_code");
            DropColumn("dbo.handappeal", "review_appeal_number");
            DropColumn("dbo.handappeal", "inspection_expert");
            DropColumn("dbo.handappeal", "inspection_kind");
            DropColumn("dbo.handappeal", "inspection_work");
            DropColumn("dbo.handappeal", "inspection_tfoms_worker");
            DropColumn("dbo.handappeal", "received_phone_number");
            DropColumn("dbo.handappeal", "received_feedback_address");
            DropColumn("dbo.handappeal", "guide_date");
            DropColumn("dbo.handappeal", "guide_medical_organization_name");
            DropColumn("dbo.handappeal", "guide_number");
            DropColumn("dbo.handappeal", "medical_organization_name");
            DropColumn("dbo.handappeal", "appeal_number");
            DropColumn("dbo.handappeal", "ozpz_registration_date");
        }
    }
}
