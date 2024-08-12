using AutoMapper;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Interfaces.Repositories;
using BankingControlPanel.Interfaces.Services;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Application.Services;

public class ClientService(IClientRepository clientRepository, IMapper mapper) : IClientService
{
    public IEnumerable<ClientDto> GetClients(string filter, string sort, int page, int pageSize)
    {
        var clients = clientRepository.GetClients(filter, sort, page, pageSize);
        return mapper.Map<IEnumerable<ClientDto>>(clients);
    }

    public ClientDto GetClientById(int id)
    {
        var client = clientRepository.GetClientById(id);
        return mapper.Map<ClientDto>(client);
    }

    public Task AddClientAsync(ClientDto clientDto)
    {
        var client = mapper.Map<Client>(clientDto);
        clientRepository.AddClient(client);
        return Task.CompletedTask;
    }

    public void UpdateClient(int id, ClientDto clientDto)
    {
        var client = clientRepository.GetClientById(id);
        if (client != null)
        {
            mapper.Map(clientDto, client);
            clientRepository.UpdateClient(client);
        }
    }

    public void DeleteClient(int id)
    {
        clientRepository.DeleteClient(id);
    }

    public IEnumerable<string> GetLastSearchParameters(int count)
    {
        return clientRepository.GetLastSearchParameters(count);
    }
}