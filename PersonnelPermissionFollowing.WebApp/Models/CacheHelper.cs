using PersonnelPermissionFollowing.BusinessLayer;
using PersonnelPermissionFollowing.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace PersonnelPermissionFollowing.WebApp.Models
{
    public class CacheHelper
    {
        public static List<Personnels> GetPersonnelsFromCache()
        {
            var personnels = WebCache.Get("personnel-cache");
            if (personnels == null)
            {
                PersonnelManager personnelManager = new PersonnelManager();
                personnels = personnelManager.ListQueryable()
                                             .Include("PersonnelDegrees").Include("PersonnelPositions").Include("PersonnelTasks")
                                             .OrderByDescending(x => x.CreatedOnDatetime).ToList();
                WebCache.Set("personnel-cache", personnels, 60, true);
            }
            return personnels;
        }

        public static int GetActivePersonnelsFromCache()
        {
            var personnels = WebCache.Get("activepersonnel-cache");
            if (personnels == null)
            {
                PersonnelManager personnelManager = new PersonnelManager();
                personnels = personnelManager.ListQueryable()
                                             .Include("PersonnelDegrees").Include("PersonnelPositions").Include("PersonnelTasks")
                                             .Where(x=>x.ExitOfJobDatetime == null)
                                             .OrderByDescending(x => x.CreatedOnDatetime).ToList().Count();
                WebCache.Set("activepersonnel-cache", personnels, 60, true);
            }
            return personnels;
        }

        public static List<PersonnelTasks> GetPersonnelTasksFromCache()
        {
            var personnelTasks = WebCache.Get("personneltasks-cache");
            if (personnelTasks == null)
            {
                PersonnelTaskManager personnelTaskManager = new PersonnelTaskManager();
                personnelTasks = personnelTaskManager.ListQueryable().OrderByDescending(m => m.CreatedOnDatetime).ToList();
                WebCache.Set("personneltasks-cache", personnelTasks, 60, true);
            }
            return personnelTasks;
        }

        public static List<PersonnelDegrees> GetPersonnelDegreesFromCache()
        {
            var personnelDegrees = WebCache.Get("personneldegrees-cache");
            if (personnelDegrees == null)
            {
                PersonnelDegreeManager personnelDegreeManager = new PersonnelDegreeManager();
                personnelDegrees = personnelDegreeManager.ListQueryable().OrderByDescending(m => m.CreatedOnDatetime).ToList();
                WebCache.Set("personneldegrees-cache", personnelDegrees, 60, true);
            }
            return personnelDegrees;
        }

        public static List<PersonnelPositions> GetPersonnelPositionsFromCache()
        {
            var personnelPositions = WebCache.Get("personnelpositions-cache");
            if (personnelPositions == null)
            {
                PersonnelPositionManager personnelPositionManager = new PersonnelPositionManager();
                personnelPositions = personnelPositionManager.ListQueryable().OrderByDescending(m => m.CreatedOnDatetime).ToList();
                WebCache.Set("personnelpositions-cache", personnelPositions, 60, true);
            }
            return personnelPositions;
        }

        public static void RemoveGetPersonnelsFromCache()
        {
            Remove("personnel-cache");
        }

        public static void RemoveGetActivePersonnelsFromCache()
        {
            Remove("activepersonnel-cache");
        }

        public static void RemoveGetPersonnelTasksFromCache()
        {
            Remove("personneltasks-cache");
        }

        public static void RemoveGetPersonnelDegreesFromCache()
        {
            Remove("personneldegrees-cache");
        }

        public static void RemoveGetPersonnelPositionsFromCache()
        {
            Remove("personnelpositions-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}