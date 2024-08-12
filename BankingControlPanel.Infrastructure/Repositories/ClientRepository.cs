using BankingControlPanel.Domain.Models;
using BankingControlPanel.Infrastructure.Data;
using BankingControlPanel.Interfaces.Repositories;

namespace BankingControlPanel.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Client> GetClients(string filter, string sort, int page, int pageSize)
    {
        var query = _context.Clients.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(c => c.FirstName.Contains(filter) || c.LastName.Contains(filter));
        }

        // Sorting and paging logic

        return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public Client GetClientById(int id)
    {
        return _context.Clients.Find(id);
    }

    public void AddClient(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
    }

    public void UpdateClient(Client client)
    {
        _context.Clients.Update(client);
        _context.SaveChanges();
    }

    public void DeleteClient(int id)
    {
        var client = _context.Clients.Find(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }
    }

    public IEnumerable<string> GetLastSearchParameters(int count)
    {
        // Logic to retrieve last search parameters
        return new List<string>();
    }
}