namespace BankingControlPanel.Domain.Models;

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public Client Client { get; set; }
    
}