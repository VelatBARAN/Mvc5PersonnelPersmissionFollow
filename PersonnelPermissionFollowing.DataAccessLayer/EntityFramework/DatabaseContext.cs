using PersonnelPermissionFollowing.DataAccessLayer.Migrations;
using PersonnelPermissionFollowing.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AssigningTaskOfPersonnel> AssigningTaskOfPersonnel { get; set; }
        public DbSet<PersonnelDegrees> PersonnelDegrees { get; set; }
        public DbSet<PersonnelPermissionTips> PersonnelPermissionTips { get; set; }
        public DbSet<PersonnelPositions> PersonnelPositions { get; set; }
        public DbSet<Personnels> Personnels { get; set; }
        public DbSet<PersonnelTasks> PersonnelTasks { get; set; }
        public DbSet<Zones> Zones { get; set; }
        public DbSet<PersonnelPermissionRequest> PersonnelPermissionRequest { get; set; }
        public DbSet<Weekdays> Weekdays { get; set; }
        public DbSet<PermissionStates> PermissionStates { get; set; }
        public DbSet<PopUps> PopUps { get; set; }

        public DatabaseContext()
        {
              //Database.SetInitializer(new MyInitializer());
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
        }

        public List<Personnels> ExecuteGetPersonnelPermissionDetails(int id)
        {
            return Database.SqlQuery<Personnels>("select * from Personnels where Id = '"+id+"'").ToList();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{          
        //    // Fluent Api - cascade 
        //        modelBuilder.Entity<Personnels>()
        //            .HasMany(n => n.Zones) // personnels tablosu zones ile 1 e çok ilişki
        //            .WithRequired(c => c.Personnels) // zones te personnels tablosu ile ilişkilidir.
        //            .WillCascadeOnDelete(true); // cascade özelliği silme

        //}
    }
}
