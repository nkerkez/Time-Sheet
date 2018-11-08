using System;
using System.Collections.Generic;

namespace TimeSheet.Services.Interfaces.Interfaces
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        bool Delete(Guid id);

    }
}
