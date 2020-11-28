namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addenewids : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActivityDataModels", "ActivitylistId_Id", "dbo.ListofActivitysModels");
            DropIndex("dbo.ActivityDataModels", new[] { "ActivitylistId_Id" });
            RenameColumn(table: "dbo.ActivityDataModels", name: "ActivitylistId_Id", newName: "LisfofActivityModelId");
            AlterColumn("dbo.ActivityDataModels", "LisfofActivityModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.ActivityDataModels", "LisfofActivityModelId");
            AddForeignKey("dbo.ActivityDataModels", "LisfofActivityModelId", "dbo.ListofActivitysModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActivityDataModels", "LisfofActivityModelId", "dbo.ListofActivitysModels");
            DropIndex("dbo.ActivityDataModels", new[] { "LisfofActivityModelId" });
            AlterColumn("dbo.ActivityDataModels", "LisfofActivityModelId", c => c.Int());
            RenameColumn(table: "dbo.ActivityDataModels", name: "LisfofActivityModelId", newName: "ActivitylistId_Id");
            CreateIndex("dbo.ActivityDataModels", "ActivitylistId_Id");
            AddForeignKey("dbo.ActivityDataModels", "ActivitylistId_Id", "dbo.ListofActivitysModels", "Id");
        }
    }
}
