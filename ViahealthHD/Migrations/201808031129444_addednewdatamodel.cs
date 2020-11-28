namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewdatamodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Activityid_ActivityId = c.Int(nullable: false),
                        Activityid_ActivityName = c.String(),
                        Activityid_Ratio = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        MyProperty = c.DateTime(nullable: false),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamsDataModels", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityDataModels", "Team_Id", "dbo.TeamsDataModels");
            DropIndex("dbo.ActivityDataModels", new[] { "Team_Id" });
            DropTable("dbo.TeamsDataModels");
            DropTable("dbo.ActivityDataModels");
        }
    }
}
