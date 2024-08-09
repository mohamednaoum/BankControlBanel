using BankingControlPanel.Domain.Enums;
using BankingControlPanel.Domain.ValueObjects;

namespace BankingControlPanel.Domain.Models;

public class Client
{
    public int Id { get; set; }
    public Email Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public PersonalId PersonalId { get; set; }
    public string ProfilePhoto { get; set; }
    public string MobileNumber { get; set; }
    public Gender Gender { get; set; }
    public int AddressId { get; set; } // Foreign key for Address
    public Address Address { get; set; } // Navigation property for Address
    public List<Account> Accounts { get; set; }
    public Client()
    {
        Accounts = new List<Account>();
    }
}