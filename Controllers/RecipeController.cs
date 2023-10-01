using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quick_recipe.Data;
using quick_recipe.DTOs;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Models;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/recipe")]
public class RecipeController : ControllerBase
{
    private readonly ApplicationContext _context;

    public RecipeController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] RecipeDTO recipeDto)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null)
        {
            return NotFound();
        }

        Recipe recipe = new()
        {
            Name = recipeDto.Name,
            TotalTimeInSeconds = 0,
            Ingredients = recipeDto.Ingredients,
            UserId = user.Id,
            MenuId = recipeDto.MenuId
        };

        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();

        return Created(nameof(Create), recipe);
    }
}