using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using System.Threading.Tasks;

namespace quick_recipe.Controllers
{
    [ApiController]
    [Route("/recipe-menu")]
    [Authorize]
    public class RecipeMenuController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public RecipeMenuController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("associate")]
        public async Task<IActionResult> AssociateRecipeToMenu(int recipeId, int menuId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            var menu = await _context.Menus.FindAsync(menuId);

            if (recipe == null || menu == null)
                return NotFound("Receita ou menu não encontrada.");

            menu.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok("Receita vinculada com sucesso.");
        }

        [HttpPost("disassociate")]
        public async Task<IActionResult> DisassociateRecipeFromMenu(int recipeId, int menuId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            var menu = await _context.Menus.FindAsync(menuId);

            if (recipe == null || menu == null)
                return NotFound("Receita ou menu não encontrada.");

            menu.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok("Receita desvinculada com sucesso.");
        }
    }
}
