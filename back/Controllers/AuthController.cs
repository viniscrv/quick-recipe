using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.DTOs;
using quick_recipe.Models;
using quick_recipe.Services;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/")]
public class AuthController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly TokenService _tokenService;

    public AuthController(ApplicationContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public IActionResult Register([FromBody] User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return Created(nameof(Register), user);
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDTO credentials)
    {
        var user = await _context.Users
            .Where(user => user.Email == credentials.Email)
            .FirstOrDefaultAsync();

        if (user == null) return NotFound();

        var credentialsMatch = user.Password == credentials.Password;

        if (!credentialsMatch)
        {
            return Unauthorized();
        }

        var token = _tokenService.Generate(user);
        
        return Ok(new { token = token });
    }
}