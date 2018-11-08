using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;


namespace TimeSheet.Services.CountryTests
{
    [TestClass]
    public class CountryServiceTests
    {

        [TestMethod]
        public void CountryService_CountryRepositoryIsNotNull_ReturnsValidObject()
        {
            var repository = Substitute.For<ICountryRepository>();

            ICountryService countryService = new CountryService(repository);
        }

        [TestMethod]
        public void CountryService_CountryRepositoryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CountryService(null));
        }


        [TestMethod]
        public void Add_CountryIsNull_ThrowsArgumentNullException()
        {
            var repository = Substitute.For<ICountryRepository>();

            Assert.ThrowsException<ArgumentNullException>(() => (new CountryService(repository)).Add(null));
        }

        [TestMethod]
        public void Add_NameOfCountryIsNull_ThrowsArgumentException()
        {

            var repository = Substitute.For<ICountryRepository>();
            Country country = new Country
            {
                Name = null

            };

            Assert.ThrowsException<ArgumentException>(() => (new CountryService(repository)).Add(country));

        }

        [TestMethod]
        public void Add_NameOfCountryIsEmpty_ThrowsArgumentException()
        {
            var repository = Substitute.For<ICountryRepository>();

            Country country = new Country
            {
                Name = String.Empty,

            };

            Assert.ThrowsException<ArgumentException>(() => (new CountryService(repository)).Add(country));
        }

        [TestMethod]
        public void Add_NameOfCountryIsCorrect_AddOperationIsSuccessful()
        {

            var repository = Substitute.For<ICountryRepository>();
            Country country = new Country
            {
                Name = "Francuska"

            };

            (new CountryService(repository)).Add(country);
            repository.Received().Add(country);
        }

        [TestMethod]
        public void Update_CountryIsNull_ThrowsArgumentNullException()
        {
            var repository = Substitute.For<ICountryRepository>();
            Assert.ThrowsException<ArgumentNullException>(() => (new CountryService(repository)).Update(null));
        }

        [TestMethod]
        public void Update_NameOfCountryIsNull_ThrowsArgumentException()
        {

            var repository = Substitute.For<ICountryRepository>();

            Country country = new Country
            {
                Name = null

            };

            Assert.ThrowsException<ArgumentException>(() => (new CountryService(repository)).Update(country));

        }

        [TestMethod]
        public void Update_NameOfCountryIsEmpty_ThrowsArgumentException()
        {

            var repository = Substitute.For<ICountryRepository>();
            Country country = new Country
            {
                Name = String.Empty,

            };

            Assert.ThrowsException<ArgumentException>(() => (new CountryService(repository)).Update(country));
        }

        [TestMethod]
        public void Update_CountryDoesNotExist_ThrowsArgumentException()
        {

            var repository = Substitute.For<ICountryRepository>();

            Country invalidCountry = new Country
            {
                Name = "Belgija",
                Id = Guid.NewGuid()
            };

            repository.When(x => x.Update(invalidCountry)).Do(x => throw new ArgumentException());
            Assert.ThrowsException<ArgumentException>(() => (new CountryService(repository)).Update(invalidCountry));

        }

        [TestMethod]
        public void Update_CountryIsCorrectAndExists_UpdateOperationIsSuccessful()
        {

            var repository = Substitute.For<ICountryRepository>();

            Guid id = Guid.NewGuid();

            Country updateCountry = new Country
            {
                Name = "Belgija",
                Id = id
            };
            (new CountryService(repository)).Update(updateCountry);

            repository.Received().Update(updateCountry);
        }

        [TestMethod]
        public void GetById_CountryDoesNotExist_ReturnsNull()
        {
            var repository = Substitute.For<ICountryRepository>();

            Guid id = Guid.NewGuid();
            repository.GetById(id).Returns(x => null);
            Country country = (new CountryService(repository)).GetById(id);

            Assert.AreEqual(null, country);

        }

        [TestMethod]
        public void GetById_CountryExists_ReturnsCountry()
        {
            var repository = Substitute.For<ICountryRepository>();

            Country sweden = new Country
            {
                Name = "Svedska",
                Id = Guid.NewGuid()
            };
     
            repository.GetById(sweden.Id).Returns(sweden);

            Country country = (new CountryService(repository)).GetById(sweden.Id);

            Assert.AreEqual(sweden, country);

        }

        [TestMethod]
        public void GetAll_ListIsEmpty_ReturnsEmptyList()
        {
            var repository = Substitute.For<ICountryRepository>();

            List<Country> allCountries = new List<Country> { };

            repository.GetAll().Returns(new List<Country> { });

            CollectionAssert.AreEqual(allCountries, (List<Country>)(new CountryService(repository)).GetAll());

        }

        [TestMethod]
        public void GetAll_ListIsNotEmpty_ReturnsList()
        {
            var repository = Substitute.For<ICountryRepository>();

            Country sweden = new Country
            {
                Name = "Svedska",
                Id = Guid.NewGuid()
            };
            List<Country> allCountries = new List<Country> { sweden };

            repository.GetAll().Returns(new List<Country> { sweden });

            CollectionAssert.AreEqual(allCountries, (List<Country>)(new CountryService(repository)).GetAll());

        }

        [TestMethod]
        public void Delete_CountryDoesNotExist_ReturnFalse()
        {

            var repository = Substitute.For<ICountryRepository>();

            Guid id = Guid.NewGuid();

            repository.Delete(id).Returns(false);

            Assert.IsFalse((new CountryService(repository)).Delete(id));
        }

        [TestMethod]
        public void Delete_CountryExists_ReturnTrue()
        {

            var repository = Substitute.For<ICountryRepository>();
       
            Guid id = Guid.NewGuid();
            repository.Delete(id).Returns(true);

            Assert.IsTrue((new CountryService(repository)).Delete(id));
        }
    }
}
