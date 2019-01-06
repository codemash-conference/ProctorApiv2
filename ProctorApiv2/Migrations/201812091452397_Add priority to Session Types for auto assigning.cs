namespace ProctorApiv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddprioritytoSessionTypesforautoassigning : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionTypes", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionTypes", "Priority");
        }
    }
}
