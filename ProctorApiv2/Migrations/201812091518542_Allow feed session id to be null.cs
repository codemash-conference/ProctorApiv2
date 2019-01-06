namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Allowfeedsessionidtobenull : DbMigration
    {
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "FeedSessionId", c => c.Int(nullable: false));
        }

        public override void Up()
        {
            AlterColumn("dbo.Sessions", "FeedSessionId", c => c.Int());
        }
    }
}
