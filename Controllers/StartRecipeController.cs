using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.Models;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/start-recipe")]
public class StartRecipeController : ControllerBase
{
    private readonly ApplicationContext _context;

    public StartRecipeController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpPost("{recipeId}")]
    public async Task<IActionResult> Start([FromRoute] int recipeId)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null) return NotFound();

        var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
        
        if (recipe == null) return NotFound();

        RecipeInProgress NewRecipeInProgress = new()
        {
            CurrentStep = 1,
            RecipeId = recipe.Id,
        };

        await _context.RecipeInProgresses.AddAsync(NewRecipeInProgress);
        
        user.RecipeInProgress = NewRecipeInProgress;
        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();

        return Ok();
    }
}