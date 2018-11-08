using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.Models
{
    public class Category : IModelBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

    }

    
}
