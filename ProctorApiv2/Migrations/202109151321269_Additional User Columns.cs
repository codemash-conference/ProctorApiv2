namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalUserColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String(maxLength: 6));
            AddColumn("dbo.AspNetUsers", "School", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Major", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "TopicsInterestedIn", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Essay", c => c.String());
            AddColumn("dbo.AspNetUsers", "PreviousVolunteer", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.AspNetUsers", "VolunteerYears", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.AspNetUsers", "Accepted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.AspNetUsers", "AcceptedDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Cancelled", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.AspNetUsers", "CancelledDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CancelledDate");
            DropColumn("dbo.AspNetUsers", "Cancelled");
            DropColumn("dbo.AspNetUsers", "AcceptedDate");
            DropColumn("dbo.AspNetUsers", "Accepted");
            DropColumn("dbo.AspNetUsers", "VolunteerYears");
            DropColumn("dbo.AspNetUsers", "PreviousVolunteer");
            DropColumn("dbo.AspNetUsers", "Essay");
            DropColumn("dbo.AspNetUsers", "TopicsInterestedIn");
            DropColumn("dbo.AspNetUsers", "Major");
            DropColumn("dbo.AspNetUsers", "School");
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}
