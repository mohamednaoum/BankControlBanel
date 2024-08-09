using AutoMapper;
using BankingControlPanel.Application.Interfaces;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Infrastructure.Repositories;
using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public IEnumerable<ClientDto> GetClients(string filter, string sort, int page, int pageSize)
    {
        var clients = _clientRepository.GetClients(filter, sort, page, pageSize);
        return _mapper.Map<IEnumerable<ClientDto>>(clients);
    }

    public ClientDto GetClientById(int id)
    {
        var client = _clientRepository.GetClientById(id);
        return _mapper.Map<ClientDto>(client);
    }

    public void AddClient(ClientDto clientDto)
    {
        var client = _mapper.Map<Client>(clientDto);
        _clientRepository.AddClient(client);
    }

    public void UpdateClient(int id, ClientDto clientDto)
    {
        var client = _clientRepository.GetClientById(id);
        if (client != null)
        {
            _mapper.Map(clientDto, client);
            _clientRepository.UpdateClient(client);
        }
    }

    public void DeleteClient(int id)
    {
        _clientRepository.DeleteClient(id);
    }

    public IEnumerable<string> GetLastSearchParameters(int count)
    {
        return _clientRepository.GetLastSearchParameters(count);
    }
}