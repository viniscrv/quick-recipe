using System.ComponentModel.DataAnnotations;

namespace quick_recipe.DTOs;

public class RecipeDTO
{
    [Required] public string Name { get; set; } = null!;
    public List<string>? Ingredients { get; set; }
    public int? MenuId { get; set; }
}