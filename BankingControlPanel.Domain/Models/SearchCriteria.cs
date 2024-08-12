namespace BankingControlPanel.Domain.Models;

public class SearchCriteria
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Criteria { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}