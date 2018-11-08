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
    public class TeamMemberServiceTests
    {
        private ITeamMemberRepository _teamMemberRepository;
        private TeamMember _teamMember;
        [TestInitialize]
        public void Init()
        {
            _teamMemberRepository = Substitute.For<ITeamMemberRepository>();
            _teamMember = new TeamMember
            {
                Name = "Name",
                Username = "Username",
                PasswordHash = "Password hash",
                HoursPerWeek = 40,
                Email = "user@example.com",
                RoleId = Guid.NewGuid(),
                Id = Guid.NewGuid()
                
            };
        }

        [TestMethod]
        public void TeamMemberService_TeamMemberRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TeamMemberService(null));
        }

        [TestMethod]
        public void TeamMemberService_TeamMemberRepositoryIsNotNull_ReturnsValidObject()
        {
            new TeamMemberService(_teamMemberRepository);
        }

        [TestMethod]
        public void Add_TeamMemberIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => (new TeamMemberService(_teamMemberRepository)).Add(null));
        }

        [TestMethod]
        public void Add_NameOfTeamMemberIsNull_ThrowsArgumentException()
        {
            _teamMember.Name = null;
            Assert.ThrowsException<ArgumentException>(() => (new TeamMemberService(_teamMemberRepository)).Add(_teamMember));
        }

        [TestMethod]
        public void Add_NameOfTeamMemberIsEmpty_ThrowsArgumentException()
        {
            _teamMember.Name = string.Empty;
            Assert.ThrowsException<ArgumentException>(() => (new TeamMemberService(_teamMemberRepository)).Add(_teamMember));
        }

        [TestMethod]
        public void Add_UsernameOfTeamMemberIsNull_ThrowsArgumentException()
        {
            _teamMember.Username = null;
            Assert.ThrowsException<ArgumentException>(() => (new TeamMemberService(_teamMemberRepository)).Add(_teamMember));
        }

        [TestMethod]
        public void Add_UsernameOfTeamMemberIsEmpty_ThrowsArgumentException()
        {
            _teamMember.PasswordHash = string.Empty;
            Assert.ThrowsException<ArgumentException>(() => (new TeamMemberService(_teamMemberRepository)).Add(_teamMember));
        }

        [TestMethod]
        public void Add_PasswordHashOfTeamMemberIsNull_ThrowsArgumentException()
        {
            _teamMember.PasswordHash = null;
            Assert.ThrowsException<ArgumentException>(() => (new TeamMemberService(_teamMemberRepository)).Add(_teamMember));
        }

        [TestMethod]
        public void Add_PasswordHashOfTeamMemberIsEmpty_ThrowsArgumentException()
        {
            _teamMember.PasswordHash = string.Empty;
            Assert.ThrowsException<ArgumentException>(() => (new TeamMemberService(_teamMemberRepository)).Add(_teamMember));
        }

        [TestMethod]
        public void Add_RoleOfTeamMemberIsEmpty_ThrowsArgumentException()
        {
            _teamMember.RoleId = Guid.Empty;
            Assert.ThrowsException<ArgumentException>(() => (new TeamMemberService(_teamMemberRepository)).Add(_teamMember));
        }

      
    }
}
