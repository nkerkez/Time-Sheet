using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Client.Interfaces.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAll();
        IEnumerable<Project> FilterByName(string name);
        IEnumerable<Project> FilterByFirstLetterOfName(char letter);
        Project GetById(Guid id);


        void Add(Project project);
        void Update(Project project);
        bool Delete(Guid id);
    }
}
