namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedteamdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teamnames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TeamsDataModels", "TeamId", c => c.Int(nullable: false));
            AddColumn("dbo.TeamsDataModels", "teamnames_Id", c => c.Int());
            CreateIndex("dbo.TeamsDataModels", "teamnames_Id");
            AddForeignKey("dbo.TeamsDataModels", "teamnames_Id", "dbo.Teamnames", "Id");
            DropColumn("dbo.TeamsDataModels", "Team");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamsDataModels", "Team", c => c.String());
            DropForeignKey("dbo.TeamsDataModels", "teamnames_Id", "dbo.Teamnames");
            DropIndex("dbo.TeamsDataModels", new[] { "teamnames_Id" });
            DropColumn("dbo.TeamsDataModels", "teamnames_Id");
            DropColumn("dbo.TeamsDataModels", "TeamId");
            DropTable("dbo.Teamnames");
        }
    }
}
