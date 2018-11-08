using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeSheet.Data.Repository;
using System.Configuration;
using System.Data.SqlClient;
using TimeSheet.Models;
using System.Collections.Generic;
using System.Linq;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Data.Tests
{


    [TestClass]
    public class CountryRepositoryTests
    {

        private ICountryRepository _repository;
        private Country _brazil;
        private  string _invalidConnectionString = "Server = PRAKTIKANT-FE\\MSSQLSERVER2016 ; Database= TimeSheetTestsInvalid;Integrated Security=true;";
        private string _validConnectionString = "Server = PRAKTIKANT-FE\\MSSQLSERVER2016 ; Database= TimeSheetTests;Integrated Security=true;";

        private void DeleteAllCountries()
        {

            using (SqlConnection conn = new SqlConnection(_validConnectionString))
            {


                using (SqlCommand comm = new SqlCommand("delete from dbo.Country", conn))
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }

            }
        }
        private void DeleteAllClients()
        {

            using (SqlConnection conn = new SqlConnection(_validConnectionString))
            {

                using (SqlCommand comm = new SqlCommand("delete from dbo.Client", conn))
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }

            }
        }

        [TestInitialize]
        public void TestInit()
        {


            DeleteAllClients();
            DeleteAllCountries();

            _brazil = new Country
            {
                Id = Guid.NewGuid(),
                Name = "Brazil"
            };
            _repository = new CountryRepository("Server = PRAKTIKANT-FE\\MSSQLSERVER2016 ; Database= TimeSheetTests;Integrated Security=true;");
        }


        [TestMethod]
        public void CountryRepository_ConnectionStringIsNull_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new CountryRepository(null));

        }


        [TestMethod]
        public void CountryRepository_ConnectionStringIsEmptyString_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new CountryRepository(String.Empty));
        }

        [TestMethod]
        public void CountryRepository_ConnectionStringIsValid_ReturnsValidObject()
        {
            CountryRepository repository = new CountryRepository(_invalidConnectionString);
        }


        [TestMethod]
        public void GetAll_InvalidConnectionString_ThrowsSqlException()
        {
            _repository = new CountryRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.GetAll());
        }

        [TestMethod]
        public void GetAll_CountriesDoNotExist_ReturnsEmptyList()
        {
            Assert.AreEqual(0, _repository.GetAll().Count());

        }

        [TestMethod]
        public void GetAll_CountriesExist_ReturnsCountries()
        {

            _repository.Add(_brazil);

            Assert.AreEqual(1, _repository.GetAll().Count());


        }


        [TestMethod]
        public void GetById_InvalidConnectionString_ThrowsSqlException()
        {
            _repository = new CountryRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.GetById(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetById_CountryDoesNotExist_ReturnsNull()
        {
            Country country = _repository.GetById(Guid.NewGuid());
            Assert.AreEqual(null, country);


        }

        [TestMethod]
        public void GetById_CountryExists_ReturnsCountry()
        {


            _repository.Add(_brazil);
            Country brazilFromDb = _repository.GetById(_brazil.Id);
            Assert.AreEqual(_brazil.Id, brazilFromDb.Id);


        }

        [TestMethod]
        public void Add_InvalidConnectionString_ThrowsSqlException()
        {
            _repository = new CountryRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.Add(_brazil));

        }

        [TestMethod]
        public void Add_CountryIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _repository.Add(null));
        }

        [TestMethod]
        public void Add_CountryNameIsNull_ThrowsArgumentException()
        {
            _brazil.Name = null;
            Assert.ThrowsException<ArgumentException>(() => _repository.Add(_brazil));
        }

        [TestMethod]
        public void Add_CountryNameIsEmpty_ThrowsArgumentException()
        {
            _brazil.Name = String.Empty;
            Assert.ThrowsException<ArgumentException>(() => _repository.Add(_brazil));
        }

        [TestMethod]
        public void Add_CountryIsCorrect_AddOperationIsSuccessful()
        {
            _repository.Add(_brazil);
        }

        [TestMethod]
        public void Update_InvalidConnectionString_ThrowsSqlException()
        {
            _repository = new CountryRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.Update(_brazil));
        }

        [TestMethod]
        public void Update_CountryIsNuLL_ThrowsArgumenentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _repository.Update(null));
        }

        [TestMethod]
        public void Update_CountryNameIsNull_ThrowsArgumentException()
        {
            _brazil.Name = null;
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(_brazil));
        }

        [TestMethod]
        public void Update_CountryNameIsEmpty_ThrowsArgumentException()
        {
            _brazil.Name = String.Empty;
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(_brazil));
        }

        [TestMethod]
        public void Update_CountryIsCorrectButCountryDoesNotExist_ThrowsArgumentException()
        {
            
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(_brazil));
        }

        [TestMethod]
        public void Update_CountryIsCorrectAndExists_UpdateOperationIsSuccessful()
        {
            _repository.Add(_brazil);
            _brazil.Name = "France";
            _repository.Update(_brazil);
        }

        [TestMethod]
        public void Delete_InvalidConnectionString_ThrowsSqlException()
        {
            _repository = new CountryRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.Delete(Guid.NewGuid()));
        }

        [TestMethod]
        public void Delete_CountryDoesNotExist_ReturnsFalse()
        {
            Assert.IsFalse(_repository.Delete(Guid.NewGuid()));
        }

        [TestMethod]
        public void Delete_CountryExists_ReturnsTrue()
        {
            _repository.Add(_brazil);

            Assert.IsTrue(_repository.Delete(_brazil.Id));
        }

        
    }
}
