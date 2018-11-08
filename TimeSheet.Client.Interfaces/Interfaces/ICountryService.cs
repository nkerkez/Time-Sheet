using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Client.Interfaces.Interfaces
{
    public interface ICountryService
    {
        // Query
        IEnumerable<Country> GetAll();
        Country GetById(Guid id);

        // Command
        void Add(Country Country);
        void Update(Country Country);
        bool Delete(Guid id);

        // CQRS
    }
}
