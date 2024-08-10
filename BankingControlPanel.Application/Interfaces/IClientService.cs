using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Application.Interfaces;

public interface IClientService
{
    IEnumerable<ClientDto> GetClients(string filter, string sort, int page, int pageSize);
    ClientDto GetClientById(int id);
    Task AddClientAsync(ClientDto clientDto);
    void UpdateClient(int id, ClientDto clientDto);
    void DeleteClient(int id);
    IEnumerable<string> GetLastSearchParameters(int count);
}