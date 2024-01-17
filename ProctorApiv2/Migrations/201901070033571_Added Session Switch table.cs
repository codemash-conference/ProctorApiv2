namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSessionSwitchtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionSwitches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromUserId = c.String(),
                        ToUserId = c.String(),
                        SessionId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        StatusChangeTime = c.DateTime(),
                        Status = c.String(),
                        RelatedSessionSwitchId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SessionSwitches");
        }
    }
}
