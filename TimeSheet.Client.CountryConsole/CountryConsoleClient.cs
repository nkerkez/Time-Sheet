using System;
using System.Collections.Generic;
using System.Linq;
//using TimeSheet.Client.Initialization;
using TimeSheet.Client.Interfaces.Interfaces;
//using TimeSheet.Data.Repository;
using TimeSheet.Models;
//using TimeSheet.Services;

namespace TimeSheet.Client.CountryConsole
{
    public class CountryConsoleClient
    {

        private ICountryService _countryService;

        public CountryConsoleClient(ICountryService countryService)
        {
            _countryService = countryService;
        }

        

        public void GetAll()
        {

            PrintAll();
            Program.Print();
        }

        private List<Country> PrintAll()
        {

            List<Country> allCountries = _countryService.GetAll().ToList();
            Console.WriteLine("Drzave:");

            allCountries.ForEach(
                c =>
                {
                    Console.WriteLine("{1}. Naziv drzave : {0} ", c.Name, allCountries.IndexOf(c) + 1);
                }
                );
            return allCountries;
        }

        public void GetById()
        {
            List<Country> allCountries = PrintAll();
            Console.WriteLine("Unesite redni broj");

            int ordinalNumber = int.Parse(Console.ReadLine());

            Country country = _countryService.GetById(allCountries.ElementAt(ordinalNumber - 1).Id);
            if (country == null)
                Console.WriteLine("Ne postoji");
            else
            {
                Console.WriteLine("Naziv drzave : {0}", country.Name);



            }

            DeleteOrUpdate(country);
            Program.Print();
        }

        private void DeleteOrUpdate(Country country)
        {
            Console.WriteLine("1. Obrisi");
            Console.WriteLine("2. Izmeni");

            switch (int.Parse(Console.ReadLine()))
            {
                case 1: Delete(country.Id);
                    break;
                case 2:
                    {

                        Console.WriteLine("Unesite naziv drzava");
                        Country updatedCountry = new Country
                        {
                            Id = country.Id,
                            Name = Console.ReadLine()
                        };
                        Update(updatedCountry);
                    };
                    break;
            }
        }
        public void Add(Country country)
        {

            _countryService.Add(country);
            Program.Print();
        }

        public void Update(Country country)
        {
            _countryService.Update(country);
        }

        public bool Delete(Guid id)
        {
            return _countryService.Delete(id);
        }

        public void Add()
        {
            Console.WriteLine("Unesite ime drzave:");
            Add(new Country { Name = Console.ReadLine() });
        }


    }
}
