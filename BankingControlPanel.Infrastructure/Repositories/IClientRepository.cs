using BankingControlPanel.Domain.Models;

namespace BankingControlPanel.Infrastructure.Repositories;

public interface IClientRepository
{
    IEnumerable<Client> GetClients(string filter, string sort, int page, int pageSize);
    Client GetClientById(int id);
    void AddClient(Client client);
    void UpdateClient(Client client);
    void DeleteClient(int id);
    IEnumerable<string> GetLastSearchParameters(int count);
}