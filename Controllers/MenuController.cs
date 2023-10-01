using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.DTOs;
using quick_recipe.Models;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/menu")]
public class MenuController : ControllerBase
{
    private readonly ApplicationContext _context;

    public MenuController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("/{id}")]
    [Authorize]
    public IActionResult GetMenu([FromRoute] int id)
    {

        var menu = _context.Menus.FirstOrDefault(m => m.Id == id);

        if (menu == null)
        {
            return NotFound();
        }

        return Ok(menu);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] MenuDTO menuDto)
    {

        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null)
        {
            return NotFound();
        }

        Menu menu = new()
        {
            Name = menuDto.Name,
            Description = menuDto.Description,
            UserId = user.Id,
            User = user
        };

        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();

        return Created(nameof(Create), new { menu });
    }
}