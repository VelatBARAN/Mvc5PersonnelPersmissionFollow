namespace PersonnelPermissionFollowing.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgr3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personnels", "IsAssignedTask", c => c.Boolean(nullable: false)); // defaultValue:false gibi değerler de verilebilir.
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personnels", "IsAssignedTask");
        }
    }
}
