namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstuff : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ActivityDataModels", name: "Activityid_Id", newName: "ActivitylistId_Id");
            RenameIndex(table: "dbo.ActivityDataModels", name: "IX_Activityid_Id", newName: "IX_ActivitylistId_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ActivityDataModels", name: "IX_ActivitylistId_Id", newName: "IX_Activityid_Id");
            RenameColumn(table: "dbo.ActivityDataModels", name: "ActivitylistId_Id", newName: "Activityid_Id");
        }
    }
}
