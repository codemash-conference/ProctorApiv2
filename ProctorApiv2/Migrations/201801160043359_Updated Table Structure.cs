namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTableStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeedSessionId = c.Int(),
                        SessionTime = c.DateTime(),
                        SessionStartTime = c.DateTime(),
                        SessionEndTime = c.DateTime(),
                        Title = c.String(),
                        Abstract = c.String(),
                        Category = c.String(),
                        VolunteersRequired = c.Int(nullable: false),
                        ActualSessionStartTime = c.DateTime(),
                        ActualSessionEndTime = c.DateTime(),
                        Attendees10 = c.Int(nullable: false),
                        Attendees50 = c.Int(nullable: false),
                        Notes = c.String(),
                        SessionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SessionTypes", t => t.SessionType_Id)
                .Index(t => t.SessionType_Id);
            
            CreateTable(
                "dbo.UserCheckIns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CheckInTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SessionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Biography = c.String(),
                        GravatarUrl = c.String(),
                        TwitterLink = c.String(),
                        GitHubLink = c.String(),
                        LinkedInProfile = c.String(),
                        BlogUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScheduleExceptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Session_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Session_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.Session_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Session_Id);
            
            CreateTable(
                "dbo.SessionRooms",
                c => new
                    {
                        Session_Id = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Session_Id, t.Room_Id })
                .ForeignKey("dbo.Sessions", t => t.Session_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.Session_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.SpeakerSessions",
                c => new
                    {
                        Speaker_Id = c.String(nullable: false, maxLength: 128),
                        Session_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Speaker_Id, t.Session_Id })
                .ForeignKey("dbo.Speakers", t => t.Speaker_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.Session_Id, cascadeDelete: true)
                .Index(t => t.Speaker_Id)
                .Index(t => t.Session_Id);
            
            CreateTable(
                "dbo.TagSessions",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Session_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Session_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.Session_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Session_Id);
            
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleExceptions", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagSessions", "Session_Id", "dbo.Sessions");
            DropForeignKey("dbo.TagSessions", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.SpeakerSessions", "Session_Id", "dbo.Sessions");
            DropForeignKey("dbo.SpeakerSessions", "Speaker_Id", "dbo.Speakers");
            DropForeignKey("dbo.Sessions", "SessionType_Id", "dbo.SessionTypes");
            DropForeignKey("dbo.SessionRooms", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.SessionRooms", "Session_Id", "dbo.Sessions");
            DropForeignKey("dbo.UserCheckIns", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.UserSessions", "Session_Id", "dbo.Sessions");
            DropForeignKey("dbo.UserSessions", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCheckIns", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TagSessions", new[] { "Session_Id" });
            DropIndex("dbo.TagSessions", new[] { "Tag_Id" });
            DropIndex("dbo.SpeakerSessions", new[] { "Session_Id" });
            DropIndex("dbo.SpeakerSessions", new[] { "Speaker_Id" });
            DropIndex("dbo.SessionRooms", new[] { "Room_Id" });
            DropIndex("dbo.SessionRooms", new[] { "Session_Id" });
            DropIndex("dbo.UserSessions", new[] { "Session_Id" });
            DropIndex("dbo.UserSessions", new[] { "User_Id" });
            DropIndex("dbo.ScheduleExceptions", new[] { "User_Id" });
            DropIndex("dbo.UserCheckIns", new[] { "UserId" });
            DropIndex("dbo.UserCheckIns", new[] { "SessionId" });
            DropIndex("dbo.Sessions", new[] { "SessionType_Id" });
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropTable("dbo.TagSessions");
            DropTable("dbo.SpeakerSessions");
            DropTable("dbo.SessionRooms");
            DropTable("dbo.UserSessions");
            DropTable("dbo.ScheduleExceptions");
            DropTable("dbo.Tags");
            DropTable("dbo.Speakers");
            DropTable("dbo.SessionTypes");
            DropTable("dbo.UserCheckIns");
            DropTable("dbo.Sessions");
            DropTable("dbo.Rooms");
        }
    }
}
