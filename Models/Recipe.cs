using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quick_recipe.Models;

public class Recipe
{
    [Key] public int RecipeId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    // multiply for 60 to convert in minutes
    public int TotalTimeInSeconds { get; set; }
    [NotMapped] public List<string>? Ingredients { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    [ForeignKey("UserId")] public int UserId { get; set; }
    [Required] public User? User { get; set; }
    [ForeignKey("MenuId")] public int MenuId { get; set; }
    public Menu? Menu { get; set; }
    [ForeignKey("RecipeId")] public ICollection<Process>? Processes { get; set; }
}