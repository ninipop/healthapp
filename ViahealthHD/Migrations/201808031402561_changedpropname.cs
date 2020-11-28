namespace ViahealthHD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedpropname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityDataModels", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.ActivityDataModels", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityDataModels", "MyProperty", c => c.DateTime(nullable: false));
            DropColumn("dbo.ActivityDataModels", "Date");
        }
    }
}
