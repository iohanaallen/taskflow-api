using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowApi.Data;
using TaskFlowApi.DTOs.Auth;
using TaskFlowApi.Models;
using TaskFlowApi.Services;

namespace TaskFlowApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthController(AppDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
        _passwordHasher = new PasswordHasher<User>();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Username e password são obrigatórios.");

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (existingUser is not null)
            return BadRequest("Usuário já existe.");

        var user = new User
        {
            Username = request.Username,
            Role = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "Usuário cadastrado com sucesso."
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user is null)
            return Unauthorized("Usuário ou senha inválidos.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Usuário ou senha inválidos.");

        var token = _tokenService.GenerateToken(user);

        return Ok(new
        {
            success = true,
            message = "Login realizado com sucesso.",
            token
        });
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        var username = User.FindFirstValue(ClaimTypes.Name);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new
        {
            success = true,
            username,
            role
        });
    }
}