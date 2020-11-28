namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedteamslink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityDataModels", "Team_Id", "dbo.TeamsDataModels");
            DropIndex("dbo.ActivityDataModels", new[] { "Team_Id" });
            DropColumn("dbo.ActivityDataModels", "Team_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityDataModels", "Team_Id", c => c.Int());
            CreateIndex("dbo.ActivityDataModels", "Team_Id");
            AddForeignKey("dbo.ActivityDataModels", "Team_Id", "dbo.TeamsDataModels", "Id");
        }
    }
}
