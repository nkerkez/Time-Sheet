using System;
using System.Collections.Generic;
using TimeSheet.Client.Interfaces.Interfaces;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces.Interfaces;

namespace TimeSheet.Services
{
    public class ClientService : IClientService
    {

        private IClientRepository _clientRepository;


        public ClientService(IClientRepository ClientRepository)
        {
            _clientRepository = ClientRepository ?? throw new ArgumentNullException(nameof(ClientRepository));
        }

        public void Add(Models.Client client)
        {
            ValidateModel(client);

            client.Id = Guid.NewGuid();

            _clientRepository.Add(client);
        }

        public bool Delete(Guid id)
        {
            return _clientRepository.Delete(id);
        }

        public IEnumerable<Models.Client> GetAll()
        {
            return _clientRepository.GetAll();
        }
        
        public Models.Client GetById(Guid id)
        {
            return _clientRepository.GetById(id);
        }

        public IEnumerable<Models.Client> FilterByFirstLetterOfName(char letter)
        {
            if (!char.IsLetter(letter))
                throw new ArgumentException("Character must be letter", nameof(letter));
            return _clientRepository.FilterByFirstLetterOfName(letter);
        }

        public IEnumerable<Models.Client> FilterByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            else
                return _clientRepository.FilterByName(name);
        }


        public void Update(Models.Client client)
        {

            ValidateModel(client);

            _clientRepository.Update(client);
        }

        private void ValidateModel(Models.Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));


            if (String.IsNullOrEmpty(client.Name))
                throw new ArgumentException("Name of client is required", nameof(client.Name));

            if (client.CountryId == Guid.Empty)
            {
                throw new ArgumentException("Country of client is required", nameof(client.CountryId));
            }
        }

    }
    
}
