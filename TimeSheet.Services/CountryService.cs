using System;
using System.Collections.Generic;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Services
{
    public class CountryService : ICountryService
    {
        private ICountryRepository _countryRepository;


        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
        }

        public void Add(Country country)
        {
            ValidateModel(country);

            country.Id = Guid.NewGuid();

            _countryRepository.Add(country);
        }

        public bool Delete(Guid id)
        {
            
            return _countryRepository.Delete(id);
        }

        public IEnumerable<Country> GetAll()
        {
            return _countryRepository.GetAll();

        }
        
        public Country GetById(Guid id)
        {

            return _countryRepository.GetById(id);

        }

        public void Update(Country country)
        {
            ValidateModel(country);
            _countryRepository.Update(country);
        }

        private void ValidateModel(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));


            if (String.IsNullOrEmpty(country.Name))
                throw new ArgumentException("Name of country is required", nameof(country));
        }

    }
}