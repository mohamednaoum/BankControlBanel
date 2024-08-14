using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Interfaces.Services;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetClients(string filter, string sort, int page, int pageSize, string userId);
    ClientDto GetClientById(int id);
    Task AddClientAsync(ClientDto clientDto);
    void UpdateClient(int id, ClientDto clientDto);
    void DeleteClient(int id);
}