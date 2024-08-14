using AutoMapper;
using BankingControlPanel.Application.Interfaces.Repositories;
using BankingControlPanel.Domain.Exceptions;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Interfaces.Services;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Application.Services;

public class ClientService(IClientRepository clientRepository, ISearchCriteriaService searchCriteriaService, ICacheService cacheService ,IMapper mapper) : IClientService
{
    public async Task<Result<IEnumerable<ClientDto>>> GetClientsAsync(string filter, string sort, int page, int pageSize, string userId)
    {
        try
        {
            var cacheKey = GenerateCacheKey(filter, sort, page, pageSize, userId);

            var cachedClients = await GetCachedClientsAsync(cacheKey);
            if (cachedClients != null && cachedClients.Any())
            {
                return Result<IEnumerable<ClientDto>>.Success(cachedClients);
            }

            var clients = clientRepository.GetClients(filter, sort, page, pageSize);
            var clientDtos = mapper.Map<IEnumerable<ClientDto>>(clients);

            SaveClientsToCache(cacheKey, clientDtos);
            await searchCriteriaService.SaveSearchCriteriaAsync($"{filter}_{sort}_{page}_{pageSize}", userId);

            return Result<IEnumerable<ClientDto>>.Success(clientDtos);

        }
        catch (Exception e)
        {
            return Result<IEnumerable<ClientDto>>.Failure(e.Message);
        }
       
    }

    private string GenerateCacheKey(string filter, string sort, int page, int pageSize, string userId)
    {
        return $"Clients_{userId}_{filter}_{sort}_{page}_{pageSize}";
    }

    private async Task<IEnumerable<ClientDto>> GetCachedClientsAsync(string cacheKey)
    {
        var cachedClients = await cacheService.GetFromCacheAsync<IEnumerable<ClientDto>>(cacheKey);
        return cachedClients;
    }

    private void SaveClientsToCache(string cacheKey, IEnumerable<ClientDto> clientDtos)
    {
       
        cacheService.Set(cacheKey, clientDtos);
    }


    public Result<ClientDto> GetClientByIdAsync(int id)
    {
        var client =  clientRepository.GetClientById(id);
        if (client == null) throw new ClientNotFoundException(id.ToString());

        var clientDto = mapper.Map<ClientDto>(client);
        return Result<ClientDto>.Success(clientDto);
    }

    public void AddClient(ClientDto clientDto)
    {
        var client = mapper.Map<Client>(clientDto);
        clientRepository.AddClient(client);
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