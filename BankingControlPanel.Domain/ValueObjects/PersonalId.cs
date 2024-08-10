namespace BankingControlPanel.Domain.ValueObjects;

public class PersonalId
{
    public string Value { get; private set; }

    public PersonalId(string personalId)
    {
        if (NotValidPersonalId(personalId))
            throw new ArgumentException("Invalid personalId");

        Value = personalId;
    }

    private static bool NotValidPersonalId(string personalId)
    {
        return string.IsNullOrEmpty(personalId);
    }
}