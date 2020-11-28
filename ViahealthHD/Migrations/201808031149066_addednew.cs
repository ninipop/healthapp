namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListofActivitysModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(),
                        Ratio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamsDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Name = c.String(),
                        Team = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ActivityDataModels", "Activityid_Id", c => c.Int());
            AddColumn("dbo.ActivityDataModels", "Team_Id", c => c.Int());
            CreateIndex("dbo.ActivityDataModels", "Activityid_Id");
            CreateIndex("dbo.ActivityDataModels", "Team_Id");
            AddForeignKey("dbo.ActivityDataModels", "Activityid_Id", "dbo.ListofActivitysModels", "Id");
            AddForeignKey("dbo.ActivityDataModels", "Team_Id", "dbo.TeamsDataModels", "Id");
            DropColumn("dbo.ActivityDataModels", "Activityid_ActivityId");
            DropColumn("dbo.ActivityDataModels", "Activityid_ActivityName");
            DropColumn("dbo.ActivityDataModels", "Activityid_Ratio");
            DropColumn("dbo.ActivityDataModels", "Team_TeamId");
            DropColumn("dbo.ActivityDataModels", "Team_Username");
            DropColumn("dbo.ActivityDataModels", "Team_Name");
            DropColumn("dbo.ActivityDataModels", "Team_Team");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityDataModels", "Team_Team", c => c.String());
            AddColumn("dbo.ActivityDataModels", "Team_Name", c => c.String());
            AddColumn("dbo.ActivityDataModels", "Team_Username", c => c.String());
            AddColumn("dbo.ActivityDataModels", "Team_TeamId", c => c.Int(nullable: false));
            AddColumn("dbo.ActivityDataModels", "Activityid_Ratio", c => c.Int(nullable: false));
            AddColumn("dbo.ActivityDataModels", "Activityid_ActivityName", c => c.String());
            AddColumn("dbo.ActivityDataModels", "Activityid_ActivityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ActivityDataModels", "Team_Id", "dbo.TeamsDataModels");
            DropForeignKey("dbo.ActivityDataModels", "Activityid_Id", "dbo.ListofActivitysModels");
            DropIndex("dbo.ActivityDataModels", new[] { "Team_Id" });
            DropIndex("dbo.ActivityDataModels", new[] { "Activityid_Id" });
            DropColumn("dbo.ActivityDataModels", "Team_Id");
            DropColumn("dbo.ActivityDataModels", "Activityid_Id");
            DropTable("dbo.TeamsDataModels");
            DropTable("dbo.ListofActivitysModels");
        }
    }
}
