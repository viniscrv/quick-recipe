using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace quick_recipe.Models;

[Index(nameof(User.Email), IsUnique = true)]
public class User
{
    public int UserId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    // public String Password { get; set; }
    public string? Biography { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}