using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IModelBase
    {
        protected string _connectionString = "Server = PRAKTIKANT-FE\\MSSQLSERVER2016; Database=TimeSheet;Integrated Security=true;";
        private List<T> items = new List<T>();

        
        public virtual void Add(T entity)
        {
            items.Add(entity);
        }

        public virtual bool Delete(Guid id)
        {
            T entity = items.SingleOrDefault(c => c.Id == id);
            if (entity == null)
                return false;
            items.Remove(entity);
            return true;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return items;
        }

        public virtual T GetById(Guid id)
        {

            return items.SingleOrDefault(c => c.Id == id);
        }

        public virtual void Update(T entity)
        {

            T oldEntity = items.SingleOrDefault(c => c.Id == entity.Id);
            if (oldEntity == null)
                throw new ArgumentException();

            Type type = typeof(T);
            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!property.Name.Equals("Id"))
                {
                    property.SetValue(oldEntity, property.GetValue(entity));
                }
            }

        }
    }
}
