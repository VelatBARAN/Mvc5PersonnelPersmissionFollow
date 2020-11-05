namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personnels", "TotalUsingAllowDay", c => c.Int(nullable: false));
            AddColumn("dbo.Personnels", "TotalRemainAllowDay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personnels", "TotalRemainAllowDay");
            DropColumn("dbo.Personnels", "TotalUsingAllowDay");
        }
    }
}
