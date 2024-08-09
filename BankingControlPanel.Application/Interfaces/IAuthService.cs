using BankingControlPanel.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BankingControlPanel.Application.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
    Task<SignInResult> LoginAsync(LoginDto loginDto);
}