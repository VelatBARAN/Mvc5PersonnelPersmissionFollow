namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PopUps", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PopUps", "IsActive");
        }
    }
}
