namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PopUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        PopUpImage = c.String(nullable: false),
                        ModifiedUsername = c.String(nullable: false, maxLength: 100),
                        CreatedOnDatetime = c.DateTime(nullable: false),
                        ModifiedOnDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PopUps");
        }
    }
}
