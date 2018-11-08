using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Client.Interfaces.Interfaces;


namespace TimeSheet.Client.Web.Controllers
{
    public class CountriesController : Controller
    {

        private  ICountryService _countryService;
        private Application application = new Application();
        public CountriesController()
        {
            _countryService = application.CountryService;
        }
        // GET: Country
        public ActionResult Index()
        {
            var countries = _countryService.GetAll();

            List<SelectListItem> listOfCountries = new List<SelectListItem>();
            foreach(var country in countries)
            {
                listOfCountries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Name});
            }
            
            return View(listOfCountries);
            
        }
    }
}