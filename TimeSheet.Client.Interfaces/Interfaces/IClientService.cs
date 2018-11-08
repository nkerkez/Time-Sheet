using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Client.Interfaces.Interfaces
{
    public interface IClientService
    {

        // Query
        IEnumerable<Models.Client> GetAll();
        IEnumerable<Models.Client> FilterByName(string name);
        IEnumerable<Models.Client> FilterByFirstLetterOfName(char letter);

        Models.Client GetById(Guid id);

        // Command
        void Add(Models.Client Country);
        void Update(Models.Client Country);
        bool Delete(Guid id);

        // CQRS
        
    }
}
