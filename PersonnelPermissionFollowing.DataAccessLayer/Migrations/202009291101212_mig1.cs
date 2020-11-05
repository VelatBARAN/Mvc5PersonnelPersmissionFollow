namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssigningTaskOfPersonnel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonesId = c.Int(nullable: false),
                        PersonnelsId = c.Int(nullable: false),
                        WeekdaysId = c.Int(nullable: false),
                        TaskDescription = c.String(nullable: false, maxLength: 2000),
                        Phone = c.String(nullable: false, maxLength: 11),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnels", t => t.PersonnelsId, cascadeDelete: true)
                .ForeignKey("dbo.Zones", t => t.ZonesId, cascadeDelete: true)
                .ForeignKey("dbo.Weekdays", t => t.WeekdaysId, cascadeDelete: true)
                .Index(t => t.ZonesId)
                .Index(t => t.PersonnelsId)
                .Index(t => t.WeekdaysId);
            
            CreateTable(
                "dbo.Personnels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tc = c.String(nullable: false, maxLength: 11),
                        Name = c.String(nullable: false, maxLength: 100),
                        Surname = c.String(nullable: false, maxLength: 100),
                        ProfilImage = c.String(),
                        StartToJobDateTime = c.DateTime(nullable: false),
                        ExitOfJobDatetime = c.DateTime(),
                        TotalWorkingYear = c.Int(nullable: false),
                        TotalAllowDay = c.Int(nullable: false),
                        IsStateWorking = c.Boolean(nullable: false),
                        PersonnelDegreesId = c.Int(nullable: false),
                        PersonnelTasksId = c.Int(nullable: false),
                        PersonnelPositionsId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 2000),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonnelDegrees", t => t.PersonnelDegreesId, cascadeDelete: true)
                .ForeignKey("dbo.PersonnelPositions", t => t.PersonnelPositionsId, cascadeDelete: true)
                .ForeignKey("dbo.PersonnelTasks", t => t.PersonnelTasksId, cascadeDelete: true)
                .Index(t => t.PersonnelDegreesId)
                .Index(t => t.PersonnelTasksId)
                .Index(t => t.PersonnelPositionsId);
            
            CreateTable(
                "dbo.PersonnelDegrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonnelPermissionRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonnelsId = c.Int(nullable: false),
                        PersonnelPermissionTipsId = c.Int(nullable: false),
                        NumberofDays = c.Int(nullable: false),
                        PermissionStartDatetime = c.DateTime(nullable: false),
                        PermissionEndDatetime = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonnelPermissionTips", t => t.PersonnelPermissionTipsId, cascadeDelete: true)
                .ForeignKey("dbo.Personnels", t => t.PersonnelsId, cascadeDelete: true)
                .Index(t => t.PersonnelsId)
                .Index(t => t.PersonnelPermissionTipsId);
            
            CreateTable(
                "dbo.PersonnelPermissionTips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonnelPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonnelTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonnelsId = c.Int(nullable: false),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        RePassword = c.String(nullable: false, maxLength: 100),
                        IsAdmin = c.Boolean(nullable: false),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnels", t => t.PersonnelsId, cascadeDelete: true)
                .Index(t => t.PersonnelsId);
            
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        PersonnelsId = c.Int(nullable: false),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Personnels", t => t.PersonnelsId, cascadeDelete: false)
                .Index(t => t.PersonnelsId);
            
            CreateTable(
                "dbo.Weekdays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ModifiedUsername = c.String(maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssigningTaskOfPersonnel", "WeekdaysId", "dbo.Weekdays");
            DropForeignKey("dbo.Zones", "PersonnelsId", "dbo.Personnels");
            DropForeignKey("dbo.AssigningTaskOfPersonnel", "ZonesId", "dbo.Zones");
            DropForeignKey("dbo.Users", "PersonnelsId", "dbo.Personnels");
            DropForeignKey("dbo.Personnels", "PersonnelTasksId", "dbo.PersonnelTasks");
            DropForeignKey("dbo.Personnels", "PersonnelPositionsId", "dbo.PersonnelPositions");
            DropForeignKey("dbo.PersonnelPermissionRequest", "PersonnelsId", "dbo.Personnels");
            DropForeignKey("dbo.PersonnelPermissionRequest", "PersonnelPermissionTipsId", "dbo.PersonnelPermissionTips");
            DropForeignKey("dbo.Personnels", "PersonnelDegreesId", "dbo.PersonnelDegrees");
            DropForeignKey("dbo.AssigningTaskOfPersonnel", "PersonnelsId", "dbo.Personnels");
            DropIndex("dbo.Zones", new[] { "PersonnelsId" });
            DropIndex("dbo.Users", new[] { "PersonnelsId" });
            DropIndex("dbo.PersonnelPermissionRequest", new[] { "PersonnelPermissionTipsId" });
            DropIndex("dbo.PersonnelPermissionRequest", new[] { "PersonnelsId" });
            DropIndex("dbo.Personnels", new[] { "PersonnelPositionsId" });
            DropIndex("dbo.Personnels", new[] { "PersonnelTasksId" });
            DropIndex("dbo.Personnels", new[] { "PersonnelDegreesId" });
            DropIndex("dbo.AssigningTaskOfPersonnel", new[] { "WeekdaysId" });
            DropIndex("dbo.AssigningTaskOfPersonnel", new[] { "PersonnelsId" });
            DropIndex("dbo.AssigningTaskOfPersonnel", new[] { "ZonesId" });
            DropTable("dbo.Weekdays");
            DropTable("dbo.Zones");
            DropTable("dbo.Users");
            DropTable("dbo.PersonnelTasks");
            DropTable("dbo.PersonnelPositions");
            DropTable("dbo.PersonnelPermissionTips");
            DropTable("dbo.PersonnelPermissionRequest");
            DropTable("dbo.PersonnelDegrees");
            DropTable("dbo.Personnels");
            DropTable("dbo.AssigningTaskOfPersonnel");
        }
    }
}
