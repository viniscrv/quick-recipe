using Microsoft.EntityFrameworkCore;

namespace quick_recipe.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public ICollection<Menu> Menus { get; } = new List<Menu>();
    public ICollection<Recipe> Recipes { get; } = new List<Recipe>();
    public RecipeInProgress? RecipeInProgress { get; set; }
}