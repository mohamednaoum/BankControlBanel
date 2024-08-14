using BankingControlPanel.Shared.Dtos;

namespace BankingControlPanel.Interfaces.Services;

public interface IClientService
{
    Task<Result<IEnumerable<ClientDto>>> GetClientsAsync(string filter, string sort, int page, int pageSize, string userId);
    Result<ClientDto> GetClientByIdAsync(int id);
    void AddClient(ClientDto clientDto);
    void UpdateClient(int id, ClientDto clientDto);
    void DeleteClient(int id);
}