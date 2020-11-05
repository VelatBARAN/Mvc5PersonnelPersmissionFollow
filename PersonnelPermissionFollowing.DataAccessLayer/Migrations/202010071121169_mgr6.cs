namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PersonnelPermissionRequest", "PermissionStatesId", c => c.Int(nullable: false));
            CreateIndex("dbo.PersonnelPermissionRequest", "PermissionStatesId");
            AddForeignKey("dbo.PersonnelPermissionRequest", "PermissionStatesId", "dbo.PermissionStates", "Id", cascadeDelete: true);
            DropColumn("dbo.AssigningTaskOfPersonnel", "IsDeleted");
            DropColumn("dbo.Personnels", "IsDeleted");
            DropColumn("dbo.PersonnelDegrees", "IsDeleted");
            DropColumn("dbo.PersonnelPermissionRequest", "IsPermissionState");
            DropColumn("dbo.PersonnelPermissionRequest", "IsDeleted");
            DropColumn("dbo.PersonnelPermissionTips", "IsDeleted");
            DropColumn("dbo.PersonnelPositions", "IsDeleted");
            DropColumn("dbo.PersonnelTasks", "IsDeleted");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Zones", "IsDeleted");
            DropColumn("dbo.Weekdays", "ModifiedUsername");
            DropColumn("dbo.Weekdays", "CreatedOnDatetime");
            DropColumn("dbo.Weekdays", "ModifiedOnDatetime");
            DropColumn("dbo.Weekdays", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weekdays", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Weekdays", "ModifiedOnDatetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Weekdays", "CreatedOnDatetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Weekdays", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Zones", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelTasks", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPositions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPermissionTips", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPermissionRequest", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelPermissionRequest", "IsPermissionState", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonnelDegrees", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Personnels", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AssigningTaskOfPersonnel", "IsDeleted", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.PersonnelPermissionRequest", "PermissionStatesId", "dbo.PermissionStates");
            DropIndex("dbo.PersonnelPermissionRequest", new[] { "PermissionStatesId" });
            DropColumn("dbo.PersonnelPermissionRequest", "PermissionStatesId");
            DropTable("dbo.PermissionStates");
        }
    }
}
