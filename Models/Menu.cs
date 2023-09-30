using System.ComponentModel.DataAnnotations;

namespace quick_recipe.Models;

public class Menu
{
    public int MenuId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}