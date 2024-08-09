using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Shared.Dtos;

public class ClientDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] [MaxLength(60)] public string FirstName { get; set; }

    [Required] [MaxLength(60)] public string LastName { get; set; }

    [Required]
    [MinLength(11)]
    [MaxLength(11)]
    public string PersonalId { get; set; }

    public string ProfilePhoto { get; set; }

    [Required] [Phone] public string MobileNumber { get; set; }

    [Required] public string Sex { get; set; }

    public AddressDto Address { get; set; }

    public List<AccountDto> Accounts { get; set; }
}