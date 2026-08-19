using LibraryHub.Application.DTOs;
using LibraryHub.Application.Interfaces;
using LibraryHub.Domain.Entities;
using LibraryHub.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(
        IGenericRepository<User> userRepository,
        IPasswordHasher passwordHasher,
        JwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var users = await _userRepository.FindAsync(
            x => x.Email == request.Email);

        var user = users.FirstOrDefault();

        if (user is null || !user.IsActive)
        {
            return Unauthorized(new
            {
                message = "E-posta veya şifre hatalı."
            });
        }

        var passwordValid = _passwordHasher.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            return Unauthorized(new
            {
                message = "E-posta veya şifre hatalı."
            });
        }

        var token = _jwtTokenService.GenerateToken(user);

        return Ok(new LoginResponseDto
        {
            Token = token,
            Email = user.Email,
            Role = user.Role
        });
    }
}