using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Client.Interfaces.Interfaces
{
    public interface ITeamMemberService
    {
        IEnumerable<TeamMember> GetAll();
        TeamMember GetById(Guid id);

        void Add(TeamMember teamMember);
        void Update(TeamMember teamMember);
        bool Delete(Guid id);
    }
}
