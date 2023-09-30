using quick_recipe.Data;
using quick_recipe.Models;

namespace quick_recipe.Repositories;

public class UserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
}