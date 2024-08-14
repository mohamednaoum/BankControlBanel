namespace BankingControlPanel.Domain.Exceptions;

public class ClientNotFoundException : Exception
{
    public ClientNotFoundException(string clientId) 
        : base($"Client with ID {clientId} was not found.") { }
}