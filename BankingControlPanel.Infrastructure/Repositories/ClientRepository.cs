using BankingControlPanel.Domain.Models;
using BankingControlPanel.Infrastructure.Data;
using BankingControlPanel.Interfaces.Repositories;

namespace BankingControlPanel.Infrastructure.Repositories;

public class ClientRepository(ApplicationDbContext context) : IClientRepository
{
    public IEnumerable<Client> GetClients(string filter, string sort, int page, int pageSize)
    {
        var query = context.Clients.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(c => c.FirstName.Contains(filter) || c.LastName.Contains(filter));
        }

        query = SortResult(sort, query);

        return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    private static IQueryable<Client> SortResult(string sort, IQueryable<Client> query)
    {
        switch (sort?.ToLower())
        {
            case "asc":
                query = query.OrderBy(c => c.Id);
                break;
            case "desc":
                query = query.OrderByDescending(c => c.Id);
                break;
            default:
                query = query.OrderBy(c => c.Id); 
                break;
        }

        return query;
    }

    public Client GetClientById(int id)
    {
        return context.Clients.Find(id);
    }

    public void AddClient(Client client)
    {
        context.Clients.Add(client);
        context.SaveChanges();
    }

    public void UpdateClient(Client client)
    {
        context.Clients.Update(client);
        context.SaveChanges();
    }

    public void DeleteClient(int id)
    {
        var client = context.Clients.Find(id);
        if (client != null)
        {
            context.Clients.Remove(client);
            context.SaveChanges();
        }
    }

    public IEnumerable<string> GetLastSearchParameters(int count)
    {
        // Logic to retrieve last search parameters
        return new List<string>();
    }
}