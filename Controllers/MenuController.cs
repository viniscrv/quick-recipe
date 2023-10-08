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
    [Route("/menu")]
    public class MenuController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public MenuController(ApplicationContext context)
        {
            _context = context;
        }

        // ... [HttpPost], [HttpGet], [HttpPut], [HttpDelete], [HttpDelete("remove-all")]

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MenuDTO menuDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Menus).FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            if (menu.UserId != user.Id)
            {
                return Forbid(); // Usuario nao tem permissao para atualizar esse menu
            }
            menu.Name = menuDto.Name;
            menu.Description = menuDto.Description;
            menu.UpdatedAt = DateTime.Now;

            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Menus).FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            if (menu.UserId != user.Id)
            {
                return Forbid(); // Usuario nao tem permissao para deletar este menu
            }

            // Desvincular todas as receitas do menu
            menu.Recipes.Clear();

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("remove-all")]
        [Authorize]
        public async Task<IActionResult> RemoveAll()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Menus).FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }
            foreach (var menu in user.Menus)
            {
                _context.Menus.Remove(menu);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
