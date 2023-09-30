using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace quick_recipe.Models;

[Index(nameof(User.Email), IsUnique = true)]
public class User
{
    [Key] public int UserId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    [ForeignKey("UserId")] public ICollection<Menu>? Menus { get; set; }
    [ForeignKey("UserId")] public ICollection<Recipe>? Recipes { get; set; }
}