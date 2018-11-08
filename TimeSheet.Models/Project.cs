using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models
{
    public class Project : IModelBase
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid LeadId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
