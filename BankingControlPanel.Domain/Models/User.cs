using BankingControlPanel.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace BankingControlPanel.Domain.Models;

public class User : IdentityUser
{
    public ICollection<Client> Clients { get; set; }
    public Roles Role { get; set; }

    public User()
    {
        Clients = new List<Client>();
    }
}