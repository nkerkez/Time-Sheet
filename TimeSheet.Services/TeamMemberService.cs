using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Models;
using TimeSheet.ServicesData.Interfaces.Interfaces;

namespace TimeSheet.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private ITeamMemberRepository _teamMemberRepository;

        public TeamMemberService(ITeamMemberRepository teamMemberRepository)
        {
            _teamMemberRepository = teamMemberRepository ?? throw new ArgumentNullException(nameof(teamMemberRepository));
        }

        public void Add(TeamMember teamMember)
        {
            Validate(teamMember);
            teamMember.Id = Guid.NewGuid();
            _teamMemberRepository.Add(teamMember);
        }

        public bool Delete(Guid id)
        {
            return _teamMemberRepository.Delete(id);
        }

        public IEnumerable<TeamMember> GetAll()
        {
            return _teamMemberRepository.GetAll();
        }

        public TeamMember GetById(Guid id)
        {
            return _teamMemberRepository.GetById(id);
        }

        public void Update(TeamMember teamMember)
        {
            Validate(teamMember);
            _teamMemberRepository.Update(teamMember);
        }

        private void Validate(TeamMember teamMember)
        {
            if (teamMember == null)
                throw new ArgumentNullException(nameof(teamMember));
            if (string.IsNullOrEmpty(teamMember.Name))
                throw new ArgumentException("Name of team member is required.", nameof(teamMember.Name));
            if (string.IsNullOrEmpty(teamMember.Username))
                throw new ArgumentException("Username of team member is required.", nameof(teamMember.Username));
            if (string.IsNullOrEmpty(teamMember.Email))
                throw new ArgumentException("Email of team member is required.", nameof(teamMember.Email));
            if (string.IsNullOrEmpty(teamMember.PasswordHash))
                throw new ArgumentException("Password of team member is required.", nameof(teamMember.PasswordHash));
            if(teamMember.HoursPerWeek <= 0)
                throw new ArgumentException("Hours per week of team member is greater then 0.", nameof(teamMember.HoursPerWeek));
            if (teamMember.RoleId == Guid.Empty)
                throw new ArgumentException("Role of team member is required.", nameof(teamMember.RoleId));

        }
    }
}
