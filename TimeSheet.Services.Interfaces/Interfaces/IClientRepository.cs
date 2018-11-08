using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Services.Interfaces.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<Client> FilterByName(string name);
        IEnumerable<Client> FilterByFirstLetterOfName(char letter);
    }
}
