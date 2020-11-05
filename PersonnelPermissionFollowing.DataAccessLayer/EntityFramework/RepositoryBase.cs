using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.DataAccessLayer.EntityFramework
{
    // SingletonPattern design 
    public class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _lockSync = new object();

        protected RepositoryBase()
        {
            db = CreateContext();
        }

        private static DatabaseContext CreateContext()
        {
            if (db == null)
            {
                lock (_lockSync)
                {
                    if(db == null)
                    {
                        db = new DatabaseContext();
                    }
                }
            }
            return db;
        }
    }
}
