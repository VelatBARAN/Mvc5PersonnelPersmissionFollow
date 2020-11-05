namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AssigningTaskOfPersonnel", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Personnels", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PersonnelDegrees", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PersonnelPermissionRequest", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PersonnelPermissionTips", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PersonnelPositions", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.PersonnelTasks", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Zones", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Weekdays", "ModifiedUsername", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Weekdays", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.Zones", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonnelTasks", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonnelPositions", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonnelPermissionTips", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonnelPermissionRequest", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.PersonnelDegrees", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.Personnels", "ModifiedUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.AssigningTaskOfPersonnel", "ModifiedUsername", c => c.String(maxLength: 100));
        }
    }
}
