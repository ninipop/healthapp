namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateddatamodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamsDataModels", "teamnames_Id", "dbo.Teamnames");
            DropIndex("dbo.TeamsDataModels", new[] { "teamnames_Id" });
            RenameColumn(table: "dbo.TeamsDataModels", name: "teamnames_Id", newName: "TeamnamesId");
            AlterColumn("dbo.TeamsDataModels", "TeamnamesId", c => c.Int(nullable: false));
            CreateIndex("dbo.TeamsDataModels", "TeamnamesId");
            AddForeignKey("dbo.TeamsDataModels", "TeamnamesId", "dbo.Teamnames", "Id", cascadeDelete: true);
            DropColumn("dbo.TeamsDataModels", "TeamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamsDataModels", "TeamId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TeamsDataModels", "TeamnamesId", "dbo.Teamnames");
            DropIndex("dbo.TeamsDataModels", new[] { "TeamnamesId" });
            AlterColumn("dbo.TeamsDataModels", "TeamnamesId", c => c.Int());
            RenameColumn(table: "dbo.TeamsDataModels", name: "TeamnamesId", newName: "teamnames_Id");
            CreateIndex("dbo.TeamsDataModels", "teamnames_Id");
            AddForeignKey("dbo.TeamsDataModels", "teamnames_Id", "dbo.Teamnames", "Id");
        }
    }
}
