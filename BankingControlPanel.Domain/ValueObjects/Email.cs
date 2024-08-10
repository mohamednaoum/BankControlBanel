using System.Text.RegularExpressions;

namespace BankingControlPanel.Domain.ValueObjects;

public class Email
{
    public string Value { get; private set; }

    // Parameterless constructor for EF Core
    private Email() { }
    public Email(string email)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format");

        Value = email;
    }

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
    
  
}