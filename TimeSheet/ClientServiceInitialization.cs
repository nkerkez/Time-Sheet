using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheet.Data.Repository;
using TimeSheet.Services;

//namespace TimeSheet.Client.Initialization
//{
//    public class ClientServiceInitialization : InitializationService<ClientService>
//    {
//        public ClientService CreateService()
//        {
//            ClientRepository repository = new ClientRepository(ConfigurationManager.ConnectionStrings["TimeSheet"].ConnectionString);

//            return new ClientService(repository);
//        }
//    }
//}
