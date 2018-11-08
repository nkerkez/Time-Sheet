using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;
using TimeSheet.ServicesData.Interfaces.Interfaces;

namespace TimeSheet.Services.Tests
{
    [TestClass]
    public class RoleServiceTests
    {
        private IRoleRepository _roleRepository;

        [TestInitialize]
        public void Init()
        {
            _roleRepository = Substitute.For<IRoleRepository>();
        }

        [TestMethod]
        public void RoleService_RoleRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new RoleService(null));
        }

        [TestMethod]
        public void RoleService_RoleRepositoryIsNotNull_ReturnsValidObject()
        {
            
            new RoleService(_roleRepository);
        }

        [TestMethod]
        public void Add_RoleIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => (new RoleService(_roleRepository)).Add(null));
        }

        [TestMethod]
        public void Add_NameOfRoleIsNull_ThrowsArgumentException()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid()
            }; 

            Assert.ThrowsException<ArgumentException>(() => new RoleService(_roleRepository).Add(role));
        }

        [TestMethod]
        public void Add_NameOfRoleIsEmpty_ThrowsArgumentException()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid(),
                Name = String.Empty
            };

            Assert.ThrowsException<ArgumentException>(() => new RoleService(_roleRepository).Add(role));
        }

        [TestMethod]
        public void Add_NameOfRoleIsCorrect_AddOperationIsSuccessful()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            };
            new RoleService(_roleRepository).Add(role);
            _roleRepository.Received().Add(role);
        }

        [TestMethod]
        public void Update_RoleIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new RoleService(_roleRepository).Update(null));
        }


        [TestMethod]
        public void Update_NameOfRoleIsNull_ThrowsArgumentException()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid()
            };

            Assert.ThrowsException<ArgumentException>(() => new RoleService(_roleRepository).Update(role));
        }

        [TestMethod]
        public void Update_NameOfRoleIsEmpty_ThrowsArgumentException()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid(),
                Name = String.Empty
            };

            Assert.ThrowsException<ArgumentException>(() => new RoleService(_roleRepository).Update(role));
        }

        [TestMethod]
        public void Update_RoleDoesNotExist_ThrowsArgumentException()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            };
            _roleRepository.When(x => x.Update(role)).Do(x => throw new ArgumentException());
            Assert.ThrowsException<ArgumentException>(() => (new RoleService(_roleRepository)).Update(role));
            
        }

        [TestMethod]
        public void Update_RoleIsCorrectAndExists_UpdateOperationIsSuccessful()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            };
            new RoleService(_roleRepository).Update(role);
            _roleRepository.Received().Update(role);
        }

        [TestMethod]
        public void GetById_ClientDoesNotExist_ReturnsNull()
        {
            Guid id = Guid.NewGuid();
            _roleRepository.GetById(id).Returns(x => null);
            Assert.AreEqual(null, (new RoleService(_roleRepository).GetById(id)));
        }

        [TestMethod]
        public void GetById_ClientDoesNotExist_ReturnsClient()
        {
            Role role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            };
            _roleRepository.GetById(role.Id).Returns(x => role);
            Assert.AreEqual(role, (new RoleService(_roleRepository).GetById(role.Id)));
        }

        [TestMethod]
        public void GetAll_ListIsEmpty_ReturnsEmptyList()
        {
            _roleRepository.GetAll().Returns(x => new List<Role>());
            Assert.AreEqual(0, (new RoleService(_roleRepository)).GetAll().Count());
        }

        [TestMethod]
        public void GetAll_ListIsNotEmpty_ReturnsList()
        {
            _roleRepository.GetAll().Returns(x => new List<Role> { new Role()});
            Assert.AreEqual(1, (new RoleService(_roleRepository)).GetAll().Count());
        }

        [TestMethod]
        public void Delete_RoleDoesNotExist_ReturnsFalse()
        {
            Guid id = Guid.NewGuid();
            _roleRepository.Delete(id).Returns(x => false);
            Assert.IsFalse((new RoleService(_roleRepository)).Delete(id));
        }

        [TestMethod]
        public void Delete_RoleExists_ReturnsTrue()
        {
            Guid id = Guid.NewGuid();
            _roleRepository.Delete(id).Returns(x => true);
            Assert.IsTrue((new RoleService(_roleRepository)).Delete(id));
        }
    }
}
