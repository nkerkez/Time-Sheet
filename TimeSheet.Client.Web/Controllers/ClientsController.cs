using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Client.Web.Models.Client;
using TimeSheet.Client.Web.Models.Filter;
using TimeSheet.Models;

namespace TimeSheet.Client.Web.Controllers
{
    public class ClientsController : Controller
    {
        private IClientService _clientService;
        private ICountryService _countryService;
        private Application application = new Application();
        private string _menuItemName = "Client";
        public ClientsController()
        {
            _clientService = application.ClientService;
            _countryService = application.CountryService;

        }

        // GET: Client
        public ActionResult Index(char? letter, string name, Guid? id)
        {
            id = id ?? Guid.Empty;
            return View(GetClientsViewModel(letter, name, null, false, (Guid)id));
        }

        public PartialViewResult GetClient(ClientViewModel clientVM)
        {
            return PartialView("_Client", clientVM);
        }

        [HttpPost]
        public ActionResult Create(ClientViewModel clientViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", GetClientsViewModel(null, null, clientViewModel, true, Guid.Empty));
            }

            TimeSheet.Models.Client client = MapClientViewModelToClient(clientViewModel);

            _clientService.Add(client);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Update(ClientViewModel clientViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Index", GetClientsViewModel(null, null, null, false, clientViewModel.Id));
            }
            TimeSheet.Models.Client client = MapClientViewModelToClient(clientViewModel);

            _clientService.Update(client);
            return RedirectToAction("Index", new { id = clientViewModel.Id });

        }

        public ActionResult Delete(Guid id)
        {

            _clientService.Delete(id);
            return RedirectToAction("Index");

        }

        private ClientsViewModel MapListOfClientsToListOfClientsViewModel(List<TimeSheet.Models.Client> clients, char? firstLetterOfSearch, IEnumerable<Country> countries, ClientViewModel clientVM, Guid idOfUpdatedClient)
        {
            var clientsVM = new ClientsViewModel
            {
                ModelHasErrors = false,
                Clients = new List<ClientViewModel>(),
                FilterViewModel = new FilterViewModel { ActionName = "Index", ControllerName = "Clients", Title = "Create new client" }
            };

            clientsVM.NewClient = clientVM ?? new ClientViewModel();
            clientsVM.NewClient.Countries = ReturnCountries(Guid.Empty, countries);


            SettingStateForButtons(clientsVM.FilterViewModel.Buttons, firstLetterOfSearch);

            foreach (var client in clients)
            {
                clientsVM.Clients.Add(MapClientToClientViewModel(client, countries, idOfUpdatedClient));
            }

            clientsVM.MenuItemName = _menuItemName;
            return clientsVM;
        }

        private void SettingStateForButtons(List<FilterLetterViewModel> buttons, char? firstLetterOfSearch)
        {
            var clients = _clientService.GetAll();

            foreach (var button in buttons)
            {

                if (firstLetterOfSearch != null && firstLetterOfSearch == button.Letter)
                {
                    button.State = State.ACTIVE;
                    continue;
                }
                if (clients.Select(c => c.Name).Any(c => c.ToLower().StartsWith(button.Letter.ToString())))
                {
                    continue;
                }
                button.State = State.DISABLED;
            }
        }

        private ClientViewModel MapClientToClientViewModel(TimeSheet.Models.Client client, IEnumerable<Country> countries, Guid idOfUpdatedClient)
        {
            ClientViewModel clientVM;
            
            clientVM = new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                PostalCode = client.PostalCode,
                City = client.City,
                CountryId = client.CountryId,
                IsUpdated = client.Id == idOfUpdatedClient

            };

            clientVM.Countries = new List<SelectListItem>();
            clientVM.Countries = ReturnCountries(client.CountryId, countries);
            return clientVM;
        }

        private List<SelectListItem> ReturnCountries(Guid countryId, IEnumerable<Country> countries)
        {

            var retVal = new List<SelectListItem>();
            foreach (var country in countries)
            {
                retVal.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Name, Selected = country.Id == countryId ? true : false });
            }
            return retVal;
        }

        private TimeSheet.Models.Client MapClientViewModelToClient(ClientViewModel clientViewModel)
        {
            return new TimeSheet.Models.Client
            {
                Id = clientViewModel.Id,
                CountryId = clientViewModel.CountryId,
                Name = clientViewModel.Name,
                Address = clientViewModel.Address,
                PostalCode = clientViewModel.PostalCode,
                City = clientViewModel.City
            };
        }

        private ClientsViewModel GetClientsViewModel(char? letter, string name, ClientViewModel clientVM, bool hasErrors, Guid idOfUpdatedClient)
        {
            List<TimeSheet.Models.Client> clients = new List<TimeSheet.Models.Client>();

            if (letter != null && char.IsLetter((char)letter))
            {
                clients = _clientService.FilterByFirstLetterOfName((char)letter).ToList();
            }
            else if (string.IsNullOrEmpty(name))
            {
                clients = _clientService.GetAll().ToList();
            }
            else
            {
                clients = _clientService.FilterByName(name).ToList();
            }

            var countries = _countryService.GetAll();
            var clientsVM = MapListOfClientsToListOfClientsViewModel(clients, letter, countries, clientVM, idOfUpdatedClient);
            clientsVM.ModelHasErrors = hasErrors;


            return clientsVM;
        }
    }
}