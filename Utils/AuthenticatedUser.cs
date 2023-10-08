using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.Models;

namespace quick_recipe.Utils;

public class AuthenticatedUser
{
    private readonly ApplicationContext _context;

    public AuthenticatedUser(ApplicationContext context)
    {
        _context = context;
    }
    
    public static void Get()
    {
        // var userEmail = User.FindFirstValue(ClaimTypes.Email);
        // var user = await _context.Users.Include(u => u.RecipeInProgress).FirstOrDefaultAsync(u => u.Email == ClaimTypes.Email);
    }
}