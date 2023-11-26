using System.ComponentModel.DataAnnotations.Schema;

namespace quick_recipe.Models;

public class Process
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public int Order { get; set; }
    // multiply for 60 to convert in minutes
    public int TimeInSeconds { get; set; } = 0;
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
}