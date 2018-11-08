using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Data.Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {

        public ClientRepository(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string can't be null or empty", nameof(connectionString));
            }

            _connectionString = connectionString;
        }


        public override IEnumerable<Client> GetAll()
        {
            List<Client> retVal = new List<Client>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("dbo.spGetClients", conn))
                {


                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            retVal.Add(MapFromSqlDataReaderToClient(rdr));
                        }
                    }
                }
            }


            return retVal;
        }


        public override Client GetById(Guid id)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("dbo.spGetClientById", conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", id);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {

                        if (!rdr.Read())
                            return null;

                        return MapFromSqlDataReaderToClient(rdr);
                    }
                }
            }


        }

        public override void Add(Client entity)
        {
            ValidateModel(entity);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("spInsertClient", conn))
                {
                    SetParameters(cmd, entity);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }


        }

        public override void Update(Client entity)
        {

            ValidateModel(entity);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                conn.Open();


                using (SqlCommand cmd = new SqlCommand("spUpdateClient", conn))
                {
                    SetParameters(cmd, entity);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    int totalRows = (int)cmd.ExecuteNonQuery();

                    if (totalRows == 0)
                        throw new ArgumentException("You try to update nonexistent client or country of the client doesn't exist", nameof(entity));
                }
            }

        }

        public override bool Delete(Guid id)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand("spDeleteClient", conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", id);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    int totalRows = (int)cmd.ExecuteNonQuery();

                    return totalRows == 1 ? true : false;
                }
            }

        }

        public IEnumerable<Client> FilterByFirstLetterOfName(char letter)
        {
            if (!char.IsLetter(letter))
                throw new ArgumentException("Character must be letter", nameof(letter));

            List<Client> retVal = new List<Client>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("dbo.spFilterClientByFirstLetterOfName", conn))
                {

                    cmd.Parameters.AddWithValue("@Letter", letter);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            retVal.Add(MapFromSqlDataReaderToClient(rdr));
                        }
                    }
                }
            }
            return retVal;
        }

        public IEnumerable<Client> FilterByName(string name)
        {

            if (name == null)
                throw new ArgumentNullException(nameof(name));
            else
            {
                List<Client> retVal = new List<Client>();


                using (SqlConnection conn = new SqlConnection(_connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("dbo.spFilterClientByName", conn))
                    {

                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                
                                retVal.Add(MapFromSqlDataReaderToClient(rdr));

                            }
                        }
                    }
                }
                return retVal;
            }
        }

        private void ValidateModel(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));


            if (String.IsNullOrEmpty(client.Name))
                throw new ArgumentException("Name of client is required", nameof(client));

            if (client.CountryId == Guid.Empty)
            {
                throw new ArgumentException("Country of client is required", nameof(client));
            }
        }

        private Client MapFromSqlDataReaderToClient(SqlDataReader rdr)
        {
            Client client = new Client
            {
                Id = (Guid)rdr["Id"],
                Name = rdr["Name"].ToString(),
                City = rdr["City"]?.ToString(),
                PostalCode = rdr["PostalCode"]?.ToString(),
                Address = rdr["Address"]?.ToString(),
                CountryId = (Guid)rdr["CountryId"]

            };
            return client;
        }

        private void SetParameters(SqlCommand cmd, Client entity)
        {
            cmd.Parameters.AddWithValue("@ClientId", entity.Id);
            cmd.Parameters.AddWithValue("@ClientName", entity.Name);
            cmd.Parameters.AddWithValue("@ClientCity", entity.City);
            cmd.Parameters.AddWithValue("@ClientAddress", entity.Address);
            cmd.Parameters.AddWithValue("@ClientPostalCode", entity.PostalCode);
            cmd.Parameters.AddWithValue("@CountryId", entity.CountryId);
        }
    }
}
