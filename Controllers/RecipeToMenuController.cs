using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;

namespace quick_recipe.Controllers
{
    [ApiController]
    [Route("/recipe-to-menu")]
    public class RecipeToMenuController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public RecipeToMenuController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("add/{recipeId}/{menuId}")]
        [Authorize]
        public async Task<IActionResult> AddRecipeToMenu([FromRoute] int recipeId,[FromRoute] int menuId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Menus).FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return NotFound();
            
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
            var menu = user.Menus.FirstOrDefault(m => m.Id == menuId);

            if (recipe == null || menu == null) return NotFound("Recipe or menu not found.");

            menu.Recipes.Add(recipe);

            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("remove/{recipeId}/{menuId}")]
        [Authorize]
        public async Task<IActionResult> RemoveRecipeFromMenu([FromRoute] int recipeId,[FromRoute] int menuId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Menus).FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return NotFound();
            
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
            var menu = user.Menus.FirstOrDefault(m => m.Id == menuId);

            if (recipe == null || menu == null) return NotFound("Recipe or menu not found");

            menu.Recipes.Remove(recipe);
            
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
