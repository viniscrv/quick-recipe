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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] MenuDTO menuDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound();
            }

            Menu menu = new()
            {
                Name = menuDto.Name,
                Description = menuDto.Description,
                UserId = user.Id,
            };

            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();

            return Created(nameof(Create), menu);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get([FromRoute] int id)
        {
            var menu = _context
                .Menus
                .Include(m => m.Recipes)
                .FirstOrDefault(m => m.Id == id);

            if (menu == null) return NotFound();

            return Ok(menu);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MenuDTO menuDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.Include(u => u.Menus).FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return NotFound();

            var menu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);

            if (menu == null) return NotFound();
            if (menu.UserId != user.Id) return Forbid();

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

            if (user == null) return NotFound();

            var menu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null) return NotFound();
            if (menu.UserId != user.Id) return Forbid();

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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return NotFound();

            var menus = _context.Menus.Include(m => m.Recipes).Where(m => m.UserId == user.Id);
            
            foreach (var menu in menus)
            {
                menu.Recipes.Clear();
                _context.Menus.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}