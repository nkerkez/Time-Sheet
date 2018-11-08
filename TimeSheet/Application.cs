using System.Configuration;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Data.Repository;
using TimeSheet.Services;

namespace TimeSheet
{
    public class Application
    {
        public ICountryService CountryService
        {
            get
            {
                return new CountryService(new CountryRepository(ConnectionString));
            }
        }

        public IClientService ClientService
        {
            get
            {
                return new ClientService(new ClientRepository(ConnectionString));
            }
        }

        private string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["TimeSheet"].ConnectionString;
            }
        }
    }
}
