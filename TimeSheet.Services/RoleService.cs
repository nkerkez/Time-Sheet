using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;
using TimeSheet.ServicesData.Interfaces.Interfaces;

namespace TimeSheet.Services
{

    public class RoleService : IRoleRepository
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public void Add(Role entity)
        {
            ValidateModel(entity);
            entity.Id = Guid.NewGuid();
            _roleRepository.Add(entity);
        }

        public bool Delete(Guid id)
        {
            return _roleRepository.Delete(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetById(Guid id)
        {
            return _roleRepository.GetById(id);
        }

        public void Update(Role entity)
        {
            ValidateModel(entity);
            _roleRepository.Update(entity);
        }

        private void ValidateModel(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            if (String.IsNullOrEmpty(role.Name))
                throw new ArgumentException("Name of role is required", nameof(role.Name));

        }
    }
}
