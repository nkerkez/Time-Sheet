using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Client.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        // Query
        IEnumerable<Category> GetAll();
        Category GetById(Guid id);

        // Command
        void Add(Category category);
        void Update(Category category);
        bool Delete(Guid id);
    
        // CQRS
    }
}
