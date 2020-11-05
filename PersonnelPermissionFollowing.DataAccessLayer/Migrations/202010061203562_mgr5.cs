namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssigningTaskOfPersonnel", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Personnels", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelDegrees", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPermissionRequest", "IsPermissionState", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPermissionRequest", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPermissionTips", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPositions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelTasks", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Zones", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Weekdays", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Weekdays", "IsDeleted");
            DropColumn("dbo.Zones", "IsDeleted");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.PersonnelTasks", "IsDeleted");
            DropColumn("dbo.PersonnelPositions", "IsDeleted");
            DropColumn("dbo.PersonnelPermissionTips", "IsDeleted");
            DropColumn("dbo.PersonnelPermissionRequest", "IsDeleted");
            DropColumn("dbo.PersonnelPermissionRequest", "IsPermissionState");
            DropColumn("dbo.PersonnelDegrees", "IsDeleted");
            DropColumn("dbo.Personnels", "IsDeleted");
            DropColumn("dbo.AssigningTaskOfPersonnel", "IsDeleted");
        }
    }
}
