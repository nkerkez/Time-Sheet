using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Models;

namespace TimeSheet.Service
{
    public class CategoryService : ICategoryService
    {

        private List<Category> _categories = new List<Category>
        {

                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Frontend"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Backend"
                }

        };
        
        public void Add(Category category)
        {
            category.Id = Guid.NewGuid();
            _categories.Add(category);
        }

        public bool Delete(Guid id)
        {
            Category category = _categories.Where(c => c.Id == id).FirstOrDefault();

            if (category != null)
            {
                _categories.Remove(category);
                return true;
            }
            return false;
        }

        public IEnumerable<Category> GetAll()
        {
           return _categories;
           
        }

        public Category GetById(Guid id)
        {

            return _categories.Where(c => c.Id == id).FirstOrDefault();
            
        }

        public void Update(Category category)
        {

            Category oldCategory = _categories.FirstOrDefault(c => c.Id == category.Id);
            oldCategory.Name = category.Name;
        }
        
    }
}
