using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using System.Collections.Generic;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Services.Tests
{
    [TestClass]
    public class ClientServiceTests
    {

        [TestMethod]
        public void ClientService_ClientRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ClientService(null));
        }


        [TestMethod]
        public void ClientService_ClientRepositoryIsNotNull_ReturnsValidObject()
        {
            var repository = Substitute.For<IClientRepository>();

            IClientService ClientService = new ClientService(repository);
        }

        [TestMethod]
        public void Add_ClientIsNull_ThrowsArgumentNullException()
        {
            var repository = Substitute.For<IClientRepository>();

            Assert.ThrowsException<ArgumentNullException>(() => (new ClientService(repository)).Add(null));
        }

        [TestMethod]
        public void Add_NameOfClientIsNull_ThrowsArgumentException()
        {

            var repository = Substitute.For<IClientRepository>();
            Models.Client Client = new Models.Client
            {
                Name = null

            };

            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Add(Client));

        }

        [TestMethod]
        public void Add_NameOfClientIsEmpty_ThrowsArgumentException()
        {
            var repository = Substitute.For<IClientRepository>();

            Models.Client Client = new Models.Client
            {
                Name = String.Empty,

            };

            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Add(Client));
        }

        [TestMethod]
        public void Add_IdOfCountryIsEmpty_ThrowsArgumentException()
        {
            var repository = Substitute.For<IClientRepository>();

            Models.Client Client = new Models.Client
            {
                Name = "Klijent",
                CountryId = Guid.Empty

            };

            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Add(Client));
        }

        [TestMethod]
        public void Add_CountryOfClientDoesNotExist_ThrowsArgumentException()
        {

            var repository = Substitute.For<IClientRepository>();

            Models.Client invalidClient = new Models.Client
            {
                Name = "Klijent1",
                CountryId = Guid.NewGuid(),
            };

            repository.When(x => x.Add(invalidClient)).Do(x => throw new ArgumentException());
            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Add(invalidClient));

        }

        [TestMethod]
        public void Add_ClientIsCorrect_AddOperationIsSuccessful()
        {

            var repository = Substitute.For<IClientRepository>();
            Models.Client Client = new Models.Client
            {
                Name = "Klijent1",
                CountryId = Guid.NewGuid()

            };

            (new ClientService(repository)).Add(Client);
            repository.Received().Add(Client);
        }


        [TestMethod]
        public void Update_ClientIsNull_ThrowsArgumentNullException()
        {
            var repository = Substitute.For<IClientRepository>();
            Assert.ThrowsException<ArgumentNullException>(() => (new ClientService(repository)).Update(null));
        }

        [TestMethod]
        public void Update_NameOfClientIsNull_ThrowsArgumentException()
        {

            var repository = Substitute.For<IClientRepository>();

            Models.Client Client = new Models.Client
            {
                Name = null

            };

            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Update(Client));

        }

        [TestMethod]
        public void Update_NameOfClientIsEmpty_ThrowsArgumentException()
        {

            var repository = Substitute.For<IClientRepository>();
            Models.Client Client = new Models.Client
            {
                Name = String.Empty,

            };

            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Update(Client));
        }

        [TestMethod]
        public void Update_IdOfCountryIsEmpty_ThrowsArgumentException()
        {
            var repository = Substitute.For<IClientRepository>();

            Models.Client Client = new Models.Client
            {
                Name = "Klijent",
                CountryId = Guid.Empty,
                Id = Guid.NewGuid()

            };

            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Update(Client));
        }

        [TestMethod]
        public void Update_CountryOfClientDoesNotExist_ThrowsArgumentException()
        {

            var repository = Substitute.For<IClientRepository>();

            Models.Client invalidClient = new Models.Client
            {
                Name = "Klijent1",
                Id = Guid.NewGuid(),
                CountryId = Guid.NewGuid(),
            };

            repository.When(x => x.Update(invalidClient)).Do(x => throw new ArgumentException());
            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Update(invalidClient));

        }

        [TestMethod]
        public void Update_ClientDoesNotExist_ThrowsArgumentException()
        {

            var repository = Substitute.For<IClientRepository>();

            Models.Client invalidClient = new Models.Client
            {
                Name = "Klijent1",
                Id = Guid.NewGuid()
            };

            repository.When(x => x.Update(invalidClient)).Do(x => throw new ArgumentException());
            Assert.ThrowsException<ArgumentException>(() => (new ClientService(repository)).Update(invalidClient));

        }

        [TestMethod]
        public void Update_ClientIsCorrectAndExists_UpdateOperationIsSuccessful()
        {

            var repository = Substitute.For<IClientRepository>();

            Guid id = Guid.NewGuid();

            Models.Client updateClient = new Models.Client
            {
                Name = "Klijent1",
                Id = id,
                CountryId = Guid.NewGuid()
            };
            (new ClientService(repository)).Update(updateClient);

            repository.Received().Update(updateClient);
        }

        [TestMethod]
        public void GetById_ClientDoesNotExist_ReturnsNull()
        {
            var repository = Substitute.For<IClientRepository>();

            Guid id = Guid.NewGuid();
            repository.GetById(id).Returns(x => null);
            Models.Client Client = (new ClientService(repository)).GetById(id);

            Assert.AreEqual(null, Client);

        }

        [TestMethod]
        public void GetById_ClientExists_ReturnsClient()
        {
            var repository = Substitute.For<IClientRepository>();

            Models.Client sweden = new Models.Client
            {
                Name = "Svedska",
                Id = Guid.NewGuid()
            };

            repository.GetById(sweden.Id).Returns(sweden);

            Models.Client Client = (new ClientService(repository)).GetById(sweden.Id);

            Assert.AreEqual(sweden, Client);

        }

        [TestMethod]
        public void GetAll_ListIsEmpty_ReturnsEmptyList()
        {
            var repository = Substitute.For<IClientRepository>();


            List<Models.Client> allCountries = new List<Models.Client> { };

            repository.GetAll().Returns(new List<Models.Client> { });

            CollectionAssert.AreEqual(allCountries, (List<Models.Client>)(new ClientService(repository)).GetAll());

        }

        [TestMethod]
        public void GetAll_ListIsNotEmpty_ReturnsList()
        {
            var repository = Substitute.For<IClientRepository>();

            Models.Client sweden = new Models.Client
            {
                Name = "Svedska",
                Id = Guid.NewGuid()
            };
            List<Models.Client> allCountries = new List<Models.Client> { sweden };

            repository.GetAll().Returns(new List<Models.Client> { sweden });

            CollectionAssert.AreEqual(allCountries, (List<Models.Client>)(new ClientService(repository)).GetAll());

        }

        #region Filter Methods

        [TestMethod]
        public void FilterByName_NameOfClientIsNull_ThrowsArgumentNullException()
        {
            var repository = Substitute.For<IClientRepository>();
            Assert.ThrowsException<ArgumentNullException>(() => (new ClientService(repository).FilterByName(null)));

        }

        [TestMethod]
        public void FilterByName_NameOfClientIsEmpty_ReturnsAllClients()
        {
            var repository = Substitute.For<IClientRepository>();

            repository.FilterByName(String.Empty).Returns(x => new List<Models.Client> { new Models.Client() });
            Assert.AreEqual(1, (new ClientService(repository)).FilterByName(String.Empty).Count());
        }

        [TestMethod]
        public void FilterByName_NameOfClientIsCorrectButClientsWhoHaveTheNameThatContainsEnteredNameDoNotExist_ReturnsEmptyList()
        {
            var repository = Substitute.For<IClientRepository>();

            repository.FilterByName("invalid").Returns(x => new List<Models.Client>());
            Assert.AreEqual(0, (new ClientService(repository)).FilterByName("invalid").Count());
            
        }

        [TestMethod]
        public void FilterByName_NameOfClientIsCorrectAndClientsWhoHaveTheNameThatContainsEnteredNameExist_ReturnsList()
        {
            var repository = Substitute.For<IClientRepository>();

            repository.FilterByName("valid").Returns(x => new List<Models.Client> { new Models.Client()});
            Assert.AreEqual(1, (new ClientService(repository)).FilterByName("valid").Count());
        }

        [TestMethod]
        public void FilterByFirstLetterOfName_FirstCharacterIsNotLetter_ThrowsArgumentException()
        {
            var repository = Substitute.For<IClientRepository>();

            Assert.ThrowsException<ArgumentException>(() => new ClientService(repository).FilterByFirstLetterOfName(char.Parse("4")));

        }

        [TestMethod]
        public void FilterByFirstLetterOfName_FirstLetterOfClientNameIsCorrectButClientsWhoHaveEnteredLetterAsTheFirstLetterOfTheNameDoNotExist_ReturnsEmptyList()
        {
            var repository = Substitute.For<IClientRepository>();

            repository.FilterByFirstLetterOfName(Char.Parse("b")).Returns(x => new List<Models.Client>());
            Assert.AreEqual(0, (new ClientService(repository)).FilterByFirstLetterOfName(Char.Parse("b")).Count());

        }

        [TestMethod]
        public void FilterByFirstLetterOfName_FirstLetterOfClientNameIsCorrectAndClientsWhoHaveEnteredLetterAsTheFirstLetterOfTheNameExist_ReturnsList()
        {
            var repository = Substitute.For<IClientRepository>();

            repository.FilterByFirstLetterOfName(Char.Parse("a")).Returns(x => new List<Models.Client> { new Models.Client()});
            Assert.AreEqual(1, (new ClientService(repository)).FilterByFirstLetterOfName(Char.Parse("a")).Count());

        }

        #endregion

        [TestMethod]
        public void Delete_ClientDoesNotExist_ReturnFalse()
        {

            var repository = Substitute.For<IClientRepository>();

            Guid id = Guid.NewGuid();

            repository.Delete(id).Returns(false);

            Assert.IsFalse((new ClientService(repository)).Delete(id));
        }

        [TestMethod]
        public void Delete_ClientExists_ReturnTrue()
        {

            var repository = Substitute.For<IClientRepository>();

            Guid id = Guid.NewGuid();
            repository.Delete(id).Returns(true);

            Assert.IsTrue((new ClientService(repository)).Delete(id));
        }
    }
}

