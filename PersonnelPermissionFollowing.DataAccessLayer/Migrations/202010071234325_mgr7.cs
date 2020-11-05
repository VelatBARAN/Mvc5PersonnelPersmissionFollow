namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PersonnelPermissionRequest", "UsersId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "AuthorityId", c => c.Int(nullable: false));
            CreateIndex("dbo.PersonnelPermissionRequest", "UsersId");
            CreateIndex("dbo.Users", "AuthorityId");
            AddForeignKey("dbo.Users", "AuthorityId", "dbo.Authorities", "Id", cascadeDelete: false);
            AddForeignKey("dbo.PersonnelPermissionRequest", "UsersId", "dbo.Users", "Id", cascadeDelete: false);
            DropColumn("dbo.Users", "IsAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsAdmin", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.PersonnelPermissionRequest", "UsersId", "dbo.Users");
            DropForeignKey("dbo.Users", "AuthorityId", "dbo.Authorities");
            DropIndex("dbo.Users", new[] { "AuthorityId" });
            DropIndex("dbo.PersonnelPermissionRequest", new[] { "UsersId" });
            DropColumn("dbo.Users", "AuthorityId");
            DropColumn("dbo.PersonnelPermissionRequest", "UsersId");
            DropTable("dbo.Authorities");
        }
    }
}
