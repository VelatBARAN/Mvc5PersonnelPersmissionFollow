using PersonnelPermissionFollowing.Common;
using PersonnelPermissionFollowing.Core.DataAccess;
using PersonnelPermissionFollowing.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = db.Set<T>();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                o.CreatedOnDatetime = DateTime.Now;
                o.ModifiedOnDatetime = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUsername();
            }
            return Save();
        }

        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                o.ModifiedOnDatetime = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUsername();
            }
            return Save();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
