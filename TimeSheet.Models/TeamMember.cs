using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models
{
    public class TeamMember : IModelBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Guid RoleId { get; set; }

        public double HoursPerWeek { get; set; }
    }
}
