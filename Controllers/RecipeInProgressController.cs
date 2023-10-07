using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.Models;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/")]
public class RecipeInProgressController : ControllerBase
{
    private readonly ApplicationContext _context;

    public RecipeInProgressController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpPost("start-recipe/{recipeId}")]
    [Authorize]
    public async Task<IActionResult> Start([FromRoute] int recipeId)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.Include(u => u.RecipeInProgress).FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null) return NotFound(new { errorMessage = "User not founded" });

        if (user.RecipeInProgress != null)
            return BadRequest(new { errorMessage = "User already has a recipe in progress" });

        var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);

        if (recipe == null) return NotFound(new { errorMessage = "Recipe not founded" });

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

    [HttpGet("current-step")]
    [Authorize]
    public async Task<IActionResult> getCurrentStep()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = await _context.Users.Include(u => u.RecipeInProgress).FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null) return NotFound(new { errorMessage = "User not founded" });
        if (user.RecipeInProgress == null)
            return BadRequest(new { errorMessage = "User does not have a recipe in progress" });

        var recipe = await _context.Recipes.Include(r => r.Processes)
            .FirstOrDefaultAsync(r => r.Id == user.RecipeInProgress.RecipeId);
        
        if (recipe == null) return NotFound(new { errorMessage = "Recipe not founded" });

        var currentProcess = recipe.Processes.FirstOrDefault(p => p.Order == user.RecipeInProgress.CurrentStep);

        return Ok(currentProcess);
    }
}