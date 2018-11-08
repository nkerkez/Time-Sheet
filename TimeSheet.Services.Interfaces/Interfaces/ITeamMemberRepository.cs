using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.ServicesData.Interfaces.Interfaces
{
    public interface ITeamMemberRepository : IRepository<TeamMember>
    {
    }
}
