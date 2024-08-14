using BankingControlPanel.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BankingControlPanel.Interfaces.Services;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
    Task<SignInResult> LoginAsync(LoginDto loginDto);
}