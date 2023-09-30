using System.ComponentModel.DataAnnotations;

namespace quick_recipe.DTOS;

public class LoginDTO
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}