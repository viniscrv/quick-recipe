using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quick_recipe.Models;

public class Process
{
    [Key] public int ProcessId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public int Order { get; set; }
    // multiply for 60 to convert in minutes
    public int TimeInSeconds { get; set; }
    [NotMapped][Required] public List<string> Ingredients { get; set; } = new List<string>();
    [ForeignKey("RecipeId")] public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}