namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityDataModels", "Team_Id", "dbo.TeamsDataModels");
            DropIndex("dbo.ActivityDataModels", new[] { "Team_Id" });
            AddColumn("dbo.ActivityDataModels", "Team_TeamId", c => c.Int(nullable: false));
            AddColumn("dbo.ActivityDataModels", "Team_Username", c => c.String());
            AddColumn("dbo.ActivityDataModels", "Team_Name", c => c.String());
            AddColumn("dbo.ActivityDataModels", "Team_Team", c => c.String());
            DropColumn("dbo.ActivityDataModels", "Team_Id");
            DropTable("dbo.TeamsDataModels");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.ActivityDataModels", "Team_Id", c => c.Int());
            DropColumn("dbo.ActivityDataModels", "Team_Team");
            DropColumn("dbo.ActivityDataModels", "Team_Name");
            DropColumn("dbo.ActivityDataModels", "Team_Username");
            DropColumn("dbo.ActivityDataModels", "Team_TeamId");
            CreateIndex("dbo.ActivityDataModels", "Team_Id");
            AddForeignKey("dbo.ActivityDataModels", "Team_Id", "dbo.TeamsDataModels", "Id");
        }
    }
}
