using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.Models;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/")]
public class UserController : ControllerBase
{
    private readonly ApplicationContext _context;

    public UserController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    [Route("me")]
    public IActionResult GetUser()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user == null)
        {
            return NotFound();
        }


        return Ok(new { user });
    }
}