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

        if (user == null) return NotFound();

        Recipe recipe = new()
        {
            Name = recipeDto.Name,
            TotalTimeInSeconds = 0,
            Ingredients = recipeDto.Ingredients,
            UserId = user.Id,
            MenuId = recipeDto.MenuId
        };

        if (recipeDto.Processes != null)
        {
            var processes = recipeDto.Processes.Select(processDto =>
            {
                var process = new Process
                {
                    Name = processDto.Name!,
                    Details = processDto.Details!,
                    TimeInSeconds = processDto.TimeInSeconds,
                    Order = processDto.Order,
                    RecipeId = recipe.Id
                };
                return process;
            }).ToList();
            
            recipe.Processes = processes;
        }

        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();

        return Created(nameof(Create), recipe);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Get([FromRoute] int id)
    {
        var recipe = _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.Processes)
            .FirstOrDefault(r => r.Id == id);

        if (recipe == null) return NotFound();

        return Ok(recipe);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RecipeDTO recipeDto)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null) return NotFound();

        recipe.Name = recipeDto.Name;
        recipe.Ingredients = recipeDto.Ingredients;
        recipe.UpdatedAt = DateTime.Now;

        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null) return NotFound();

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        return Ok();
    }
}