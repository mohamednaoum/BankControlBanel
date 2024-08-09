using System.ComponentModel.DataAnnotations;
using BankingControlPanel.Domain.Enums;

namespace BankingControlPanel.Shared.Dtos;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    public Roles Role { get; set; }
}