using PersonnelPermissionFollowing.DataAccessLayer.EntityFramework;
using PersonnelPermissionFollowing.Entities;
using System.Collections.Generic;

namespace PersonnelPermissionFollowing.BusinessLayer
{
    public class Test
    {
        public List<PersonnelPositions> ListPersonnelPositions()
        {
            Repository<PersonnelPositions> listPersonnelPositions = new Repository<PersonnelPositions>();

            return listPersonnelPositions.List();
        }
    }
}
