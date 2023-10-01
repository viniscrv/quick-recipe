using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quick_recipe.Data;
using quick_recipe.DTOs;
using quick_recipe.Models;

namespace quick_recipe.Controllers;

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
    public IActionResult Create([FromBody] MenuDTO menuDto)
    {

        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user == null)
        {
            return NotFound();
        }

        var menu = new Menu
        {
            Name = menuDto.Name,
            Description = menuDto.Description,
            UserId = user.Id,
        };

        user.Menus.Add(menu);

        _context.Menus.Add(menu);
        _context.SaveChanges();

        return Created(nameof(Create), new { message = "Criado com sucesso" });
    }
    // var jsonSerializerOptions = new JsonSerializerOptions
    // {
    //     ReferenceHandler = ReferenceHandler.Preserve,
    // };

    // var jsonMenu = JsonSerializer.Serialize(menu, jsonSerializerOptions);
}