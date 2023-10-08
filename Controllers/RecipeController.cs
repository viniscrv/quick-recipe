using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.DTOs;
using quick_recipe.Models;

namespace quick_recipe.Controllers
{
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
                int order = 1;
                // Boolean previousIsSequential = false;

                var processes = recipeDto.Processes.Select((processDto, index) =>
                {
                    if (index != 0 && processDto.isSequential) order += 1;

                    var process = new Process
                    {
                        Name = processDto.Name!,
                        Details = processDto.Details!,
                        TimeInSeconds = processDto.TimeInSeconds,
                        Order = order,
                        RecipeId = recipe.Id,
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
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Recipes).FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null) return NotFound();
            if (recipe.UserId != user.Id) return Forbid();

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
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Recipes).FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.Include(r => r.Ingredients).Include(r => r.Processes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.UserId != user.Id)
            {
                return Forbid(); // Usuário não está autorizado a excluir esta receita
            }

            // Remover ingredientes associados à receita
            // if (recipe.Ingredients != null)
            // {
            //     _context.Ingredients.RemoveRange(recipe.Ingredients);
            // }

            // Remover processos associados à receita
            _context.Processes.RemoveRange(recipe.Processes);

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}