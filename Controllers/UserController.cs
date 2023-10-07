using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/")]
public class UserController : ControllerBase
{
    private readonly ApplicationContext _context;

    public UserController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users
            .Include(u => u.Menus)
            .Include(u => u.Recipes)
            .Include(u => u.RecipeInProgress)
            .FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null) return NotFound();

        return Ok(user);
    }
}