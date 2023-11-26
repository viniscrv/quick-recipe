using System.ComponentModel.DataAnnotations;

namespace quick_recipe.DTOs;

public class MenuDTO
{
    [Required] public string Name { get; set; } = null!;
    [Required] public string Description { get; set; }  = null!;
}