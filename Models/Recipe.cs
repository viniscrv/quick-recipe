using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quick_recipe.Models;

public class Recipe
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    // multiply for 60 to convert in minutes
    public int TotalTimeInSeconds { get; set; }
    [NotMapped] public List<string>? Ingredients { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int MenuId { get; set; }
    public Menu Menu { get; set; } = null!;
    public ICollection<Process> Processes { get; } = new List<Process>();
}
