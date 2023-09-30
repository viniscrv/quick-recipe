using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using quick_recipe.Data;

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
        return Ok(new {userEmail});
    }
}