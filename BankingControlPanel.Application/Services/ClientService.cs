using AutoMapper;
using BankingControlPanel.Application.Interfaces.Repositories;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Interfaces.Services;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Application.Services;

public class ClientService(IClientRepository clientRepository, ISearchCriteriaService searchCriteriaService, ICacheService cacheService ,IMapper mapper) : IClientService
{
    public async Task<IEnumerable<ClientDto>> GetClients(string filter, string sort, int page, int pageSize, string userId)
    {
        var cacheKey = GenerateCacheKey(filter, sort, page, pageSize, userId);

        var cachedClients = await GetCachedClientsAsync(cacheKey);
        if (cachedClients.Any())
        {
            return cachedClients;
        }

        var clients = clientRepository.GetClients(filter, sort, page, pageSize);
        var clientDtos = mapper.Map<IEnumerable<ClientDto>>(clients);

        SaveClientsToCache(cacheKey, clientDtos);
        await searchCriteriaService.SaveSearchCriteriaAsync($"{filter}_{sort}_{page}_{pageSize}", userId);

        return clientDtos;
    }

    private string GenerateCacheKey(string filter, string sort, int page, int pageSize, string userId)
    {
        return $"Clients_{userId}_{filter}_{sort}_{page}_{pageSize}";
    }

    private async Task<IEnumerable<ClientDto>> GetCachedClientsAsync(string cacheKey)
    {
        var cachedClients = await cacheService.GetFromCacheAsync<IEnumerable<ClientDto>>(cacheKey);
        return cachedClients ?? Enumerable.Empty<ClientDto>();
    }

    private void SaveClientsToCache(string cacheKey, IEnumerable<ClientDto> clientDtos)
    {
       
        cacheService.Set(cacheKey, clientDtos);
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