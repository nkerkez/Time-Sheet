using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Data.Repository
{
    public class CategoryRepository : RepositoryBase<Category> , ICategoryRepository
    {
    }
}
