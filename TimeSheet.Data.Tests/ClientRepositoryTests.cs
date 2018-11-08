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
    public class ClientRepositoryTests
    {

        private IClientRepository _repository;
        private Client _clockWork;
        private Guid id = Guid.NewGuid();
        private string _invalidConnectionString = "Server = PRAKTIKANT-FE\\MSSQLSERVER2016 ; Database= TimeSheetTestsInvalid;Integrated Security=true;";
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
        private void AddCountry()
        {

            using (SqlConnection conn = new SqlConnection(_validConnectionString))
            {

                
                using (SqlCommand comm = new SqlCommand("dbo.spInsertCountry", conn))
                {
                    conn.Open();
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@CountryId", id);
                    comm.Parameters.AddWithValue("@CountryName", "Brazil");
                    comm.ExecuteNonQuery();
                }

            }
        }


        [TestInitialize]
        public void TestInit()
        {
            DeleteAllClients();
            DeleteAllCountries();
            AddCountry();

            _clockWork = new Client
            {
                Id = Guid.NewGuid(),
                Name = "Clock Work",
                CountryId = id,
                City = "Novi Sad",
                PostalCode = "21000",
                Address = "Novosadskog sajma 11"
            };
            _repository = new ClientRepository("Server = PRAKTIKANT-FE\\MSSQLSERVER2016 ; Database= TimeSheetTests;Integrated Security=true;");
        }

        #region constructor tests

        [TestMethod]
        public void ClientRepository_ConnectionStringIsNull_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new ClientRepository(null));

        }


        [TestMethod]
        public void ClientRepository_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new ClientRepository(String.Empty));
        }

        [TestMethod]
        public void ClientRepository_ConnectionStringIsCorrect_ReturnsValidObject()
        {
            ClientRepository repository = new ClientRepository(_invalidConnectionString);
        }
        #endregion

        #region getAll tests
        [TestMethod]
        public void GetAll_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsSqlException()
        {
            _repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.GetAll());
        }

        [TestMethod]
        public void GetAll_ClientsDoNotExist_ReturnsEmptyList()
        {
            Assert.AreEqual(0, _repository.GetAll().Count());

        }

        [TestMethod]
        public void GetAll_ClientsExist_ReturnsClients()
        {

            _repository.Add(_clockWork);

            Assert.AreEqual(1, _repository.GetAll().Count());


        }

        #endregion

        #region getById tests
        [TestMethod]
        public void GetById_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsSqlException()
        {
            _repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.GetById(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetById_ClientDoesNotExist_ReturnsNull()
        {
            Client Client = _repository.GetById(Guid.NewGuid());
            Assert.AreEqual(null, Client);


        }

        [TestMethod]
        public void GetById_ClientExists_ReturnsClient()
        {


            _repository.Add(_clockWork);
            Client clientFromDb = _repository.GetById(_clockWork.Id);
            Assert.AreEqual(_clockWork.Id, clientFromDb.Id);


        }
        #endregion

        #region filterByName tests
        [TestMethod]
        public void FilterByName_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsSqlExceptionException()
        {
            ClientRepository repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => repository.FilterByName("name"));
        }

        [TestMethod]
        public void FilterByName_NameOfClientIsNull_ThrowsArgumentNullException()
        {
            ClientRepository repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<ArgumentNullException>(() => repository.FilterByName(null));
        }
        

        [TestMethod]
        public void FilterByName_NameOfClientIsEmpty_ReturnsList()
        {
            ClientRepository repository = new ClientRepository(_validConnectionString);
            Client client1 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                CountryId = id
            };
            Client client2 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Pandora",
                CountryId = id
            };
            repository.Add(client1);
            repository.Add(client2);

            Assert.AreEqual(2, repository.FilterByName("").Count());
        }

        [TestMethod]
        public void FilterByName_NameOfClientIsCorrectButClientsWhoHaveTheNameThatContainsEnteredNameDoNotExist_ReturnsEmptyList()
        {
            ClientRepository repository = new ClientRepository(_validConnectionString);
            Client client1 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                CountryId = id
            };
            Client client2 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Pandora",
                CountryId = id
            };
            repository.Add(client1);
            repository.Add(client2);

            Assert.AreEqual(0, repository.FilterByName("xyz").Count());
        }

        [TestMethod]
        public void FilterByName_NameOfClientIsCorrectAndClientsWhoHaveTheNameThatContainsEnteredNameExist_ReturnsList()
        {
            ClientRepository repository = new ClientRepository(_validConnectionString);
            Client client1 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                CountryId = id
            };
            Client client2 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Pandora",
                CountryId = id
            };
            repository.Add(client1);
            repository.Add(client2);

            Assert.AreEqual(1, repository.FilterByName("ndora").Count());
        }

        #endregion

        #region filterByFirstLetterOfName tests

        [TestMethod]
        public void FilterByFirstLetterOfName_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsSqlExceptionException()
        {
            ClientRepository repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => repository.FilterByFirstLetterOfName(char.Parse("m")));
        }

        [TestMethod]
        public void FilterByFirstLetterOfName_FirstCharacterOfClientNameIsNotLetter_ThrowsArgumentException()
        {
            ClientRepository repository = new ClientRepository(_validConnectionString);

            Assert.ThrowsException<ArgumentException>(() => repository.FilterByFirstLetterOfName(char.Parse("4")));
        }


        [TestMethod]
        public void FilterByFirstLetterOfName_FirstLetterOfClientNameIsCorrectButClientsWhoHaveEnteredLetterAsTheFirstLetterOfTheNameDoNotExist_ReturnsEmptyList()
        {
            ClientRepository repository = new ClientRepository(_validConnectionString);
            Client client1 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                CountryId = id
            };
            Client client2 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Pandora",
                CountryId = id
            };
            repository.Add(client1);
            repository.Add(client2);

            Assert.AreEqual(0, repository.FilterByFirstLetterOfName(char.Parse("r")).Count());
        }
        
        [TestMethod]
        public void FilterByFirstLetterOfName_FirstLetterOfClientNameIsCorrectAndClientsWhoHaveEnteredLetterAsTheFirstLetterOfTheNameExist_ReturnsList()
        {
            ClientRepository repository = new ClientRepository(_validConnectionString);
            Client client1 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                CountryId = id
            };
            Client client2 = new Client()
            {
                Id = Guid.NewGuid(),
                Name = "Pandora",
                CountryId = id
            };
            repository.Add(client1);
            repository.Add(client2);

            Assert.AreEqual(1, repository.FilterByFirstLetterOfName(char.Parse("p")).Count());
        }
        
        #endregion

        #region add operation tests
        [TestMethod]
        public void Add_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsSqlException()
        {
            _repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.Add(_clockWork));

        }

        [TestMethod]
        public void Add_ClientIsNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _repository.Add(null));
        }

        [TestMethod]
        public void Add_NameOfClientIsNull_ThrowsArgumentException()
        {
            _clockWork.Name = null;
            Assert.ThrowsException<ArgumentException>(() => _repository.Add(_clockWork));
        }

        [TestMethod]
        public void Add_NameOfClientIsEmpty_ThrowsArgumentException()
        {
            _clockWork.Name = String.Empty;
            Assert.ThrowsException<ArgumentException>(() => _repository.Add(_clockWork));
        }

        [TestMethod]
        public void Add_CountryOfClientIsEmpty_ThrowsArgumentException()
        {
            _clockWork.CountryId = Guid.Empty;
            Assert.ThrowsException<ArgumentException>(() => _repository.Add(_clockWork));
        }

        [TestMethod]
        public void Add_CountryOfClientDoesNotExist_ThrowsSqlException()
        {
            _clockWork.CountryId = Guid.NewGuid();
            Assert.ThrowsException<SqlException>(() => _repository.Add(_clockWork));
        }

        [TestMethod]
        public void Add_CityOfClientIsNull_AddOperationIsSuccessful()
        {
            _clockWork.City = null;
            _repository.Add(_clockWork);
        }

        [TestMethod]
        public void Add_PostalCodeOfClientIsNull_AddOperationIsSuccessful()
        {
            _clockWork.PostalCode = null;
            _repository.Add(_clockWork);
        }
        [TestMethod]
        public void Add_AddressClientIsNull_AddOperationIsSuccessful()
        {
            _clockWork.Address = null;
            _repository.Add(_clockWork);
        }
        [TestMethod]
        public void Add_ClientIsCorrect_AddOperationIsSuccessful()
        {
            _repository.Add(_clockWork);
        }

        #endregion
        
        #region update operation tests
        [TestMethod]
        public void Update_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsSqlException()
        {
            _repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.Update(_clockWork));
        }

        [TestMethod]
        public void Update_ClientIsNull_ThrowsArgumenentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _repository.Update(null));
        }
        [TestMethod]
        public void Update_NameOfClientIsEmpty_ThrowsArgumentException()
        {
            _clockWork.Name = String.Empty;
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(_clockWork));
        }
        [TestMethod]
        public void Update_NameOfClientIsNull_ThrowsArgumentException()
        {
            _clockWork.Name = null;
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(_clockWork));
        }

        [TestMethod]
        public void Update_CountryOfClientIsEmpty_ThrowsArgumentException()
        {
            _clockWork.CountryId = Guid.Empty;
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(_clockWork));
        }

        [TestMethod]
        public void Update_CountryOfClientDoesNotExist_SqlException()
        {
            _repository.Add(_clockWork);
            _clockWork.Name = "klijent2";
            _clockWork.CountryId = Guid.NewGuid();
            Assert.ThrowsException<SqlException>(() => _repository.Update(_clockWork));
        }

        [TestMethod]
        public void Update_ClientDoesNotExist_ThrowsArgumentException()
        {
            
            Assert.ThrowsException<ArgumentException>(() => _repository.Update(_clockWork));
        }

        [TestMethod]
        public void Update_PostalCodeOfClientIsNull_UpdateOperationIsSuccessful()
        {
            _repository.Add(_clockWork);
            _clockWork.PostalCode = null;
            _repository.Update(_clockWork);
        }
        [TestMethod]
        public void Update_UpdateressClientIsNull_UpdateOperationIsSuccessful()
        {
            _repository.Add(_clockWork);
            _clockWork.Address = null;
            _repository.Update(_clockWork);
        }
        [TestMethod]
        public void Update_ClientIsCorrectAndExists_UpdateOperationIsSuccessful()
        {
            _repository.Add(_clockWork);
            _clockWork.Name = "klijent2";
            _repository.Update(_clockWork);
        }

        
        #endregion

        #region delete operation tests
        [TestMethod]
        public void Delete_DatabaseWithEnteredConnectionStringDoesNotExist_ThrowsSqlException()
        {
            _repository = new ClientRepository(_invalidConnectionString);
            Assert.ThrowsException<SqlException>(() => _repository.Delete(Guid.NewGuid()));
        }

        [TestMethod]
        public void Delete_ClientDoesNotExist_ReturnsFalse()
        {
            Assert.IsFalse(_repository.Delete(Guid.NewGuid()));
        }

        [TestMethod]
        public void Delete_ClientExists_ReturnsTrue()
        {
            _repository.Add(_clockWork);

            Assert.IsTrue(_repository.Delete(_clockWork.Id));
        }

        #endregion
    
    }
}
