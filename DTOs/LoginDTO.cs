using System.ComponentModel.DataAnnotations;

namespace quick_recipe.DTOs;

public class LoginDTO
{
    [Required] public string? Email { get; set; }
    [Required] public string? Password { get; set; }
}