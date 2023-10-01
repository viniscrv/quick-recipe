using System.ComponentModel.DataAnnotations;
using quick_recipe.Models;

namespace quick_recipe.DTOs;

public class RecipeDTO
{
    [Required] public string Name { get; set; } = null!;
    public List<Ingredient>? Ingredients { get; set; }
    public int? MenuId { get; set; }
}