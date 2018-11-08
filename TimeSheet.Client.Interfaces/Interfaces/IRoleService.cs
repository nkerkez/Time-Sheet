using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Client.Interfaces.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(Guid id);

        void Add(Role role);
        void Update(Role role);
        bool Delete(Guid id);

    }
}
