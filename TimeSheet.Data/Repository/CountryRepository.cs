using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Data.Repository
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {

        public CountryRepository(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string can't be null or empty", nameof(connectionString));
            }

            _connectionString = connectionString;
        }


        public override IEnumerable<Country> GetAll()
        {
            List<Country> retVal = new List<Country>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //   SqlCommand cmd = new SqlCommand("Select * from dbo.Country", conn);
                using (SqlCommand cmd = new SqlCommand("dbo.spGetCountries", conn))
                {


                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            retVal.Add(MapFromSqlDataReaderToCountry(rdr));
                        }
                    }
                }
            }


            return retVal;
        }


        public override Country GetById(Guid id)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // SqlCommand cmd = new SqlCommand("Select * from dbo.Country where Id = @CountryId", conn);
                using (SqlCommand cmd = new SqlCommand("dbo.spGetCountryById", conn))
                {
                    cmd.Parameters.AddWithValue("@CountryId", id);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {

                        if (!rdr.Read())
                            return null;

                        return MapFromSqlDataReaderToCountry(rdr);
                    }
                }
            }


        }

        public override void Add(Country entity)
        {
            ValidateModel(entity);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //   SqlCommand cmd = new SqlCommand("Insert into dbo.Country values(@CountryId, @CountryName)", conn);

                using (SqlCommand cmd = new SqlCommand("spInsertCountry", conn))
                {
                    cmd.Parameters.AddWithValue("@CountryId", entity.Id);
                    cmd.Parameters.AddWithValue("@CountryName", entity.Name);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }


        }

        public override void Update(Country entity)
        {

            ValidateModel(entity);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // SqlCommand cmd = new SqlCommand("Update dbo.Country set Name = @CountryName where id = @CountryId", conn);
                conn.Open();


                using (SqlCommand cmd = new SqlCommand("spUpdateCountry", conn))
                {
                    cmd.Parameters.AddWithValue("@CountryId", entity.Id);
                    cmd.Parameters.AddWithValue("@CountryName", entity.Name);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    int totalRows = (int)cmd.ExecuteNonQuery();

                    if (totalRows == 0)
                        throw new ArgumentException("You try to update nonexistent country", nameof(entity));
                }
            }

        }

        public override bool Delete(Guid id)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                // SqlCommand cmd = new SqlCommand("Delete from dbo.Country where id = @CountryId", conn);
                using (SqlCommand cmd = new SqlCommand("spDeleteCountry", conn))
                {
                    cmd.Parameters.AddWithValue("@CountryId", id);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    int totalRows = (int)cmd.ExecuteNonQuery();

                    return totalRows == 1 ? true : false;
                }
            }

        }

        private void ValidateModel(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));


            if (String.IsNullOrEmpty(country.Name))
                throw new ArgumentException("Name of country is required", nameof(country));
        }

        private Country MapFromSqlDataReaderToCountry(SqlDataReader rdr)
        {
            Country country = new Country
            {
                Id = (Guid)rdr["Id"],
                Name = rdr["Name"].ToString()
            };
            return country;
        }
    }
}
