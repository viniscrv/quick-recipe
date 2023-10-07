using quick_recipe.Models;

namespace quick_recipe.DTOs;

public class ProcessDTO
{
    public string? Name { get; set; }
    public string? Details { get; set; }
    public int TimeInSeconds { get; set; }
    public int Order { get; set; }
    public int? RecipeId { get; set; }
}