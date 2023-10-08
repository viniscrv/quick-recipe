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

        // ... [HttpPost], [HttpGet], [HttpPut], [HttpDelete]

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RecipeDTO recipeDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Recipes).FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.UserId != user.Id)
            {
                return Forbid(); // Usuario nao permitido para atualizar
            }

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

            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.UserId != user.Id)
            {
                return Forbid(); // Usuario nao esta permitido para deletar 
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
