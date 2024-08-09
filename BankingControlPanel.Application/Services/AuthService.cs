using BankingControlPanel.Application.Interfaces;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Shared.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BankingControlPanel.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            Role = registerDto.Role
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, registerDto.Role.ToString());
        }

        return result;
    }

    public async Task<SignInResult> LoginAsync(LoginDto loginDto)
    {
        return await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
    }
}