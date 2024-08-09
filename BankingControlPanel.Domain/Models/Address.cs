namespace BankingControlPanel.Domain.ValueObjects;

public class Address
{
    public int Id { get; set; }  // Unique identifier for the Address entity
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }

    public Address(string country, string city, string street, string zipCode)
    {
        Country = country;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }
}
