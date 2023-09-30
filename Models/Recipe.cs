using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace quick_recipe.Models;

[Keyless]
public class Recipe
{
    public int RecipeId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    // multiply for 60 to convert in minutes
    public int TotalTimeInSeconds { get; set; }
    public List<string>? Ingredients { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}