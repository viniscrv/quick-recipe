using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quick_recipe.Models;

public class Menu
{
    [Key] public int MenuId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    [ForeignKey("UserId")] public int UserId { get; set; }
    public User? User { get; set; }
    [ForeignKey("MenuId")] public ICollection<Recipe>? Recipes { get; set; }
}