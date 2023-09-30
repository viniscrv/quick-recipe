using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace quick_recipe.Models;

[Keyless]
public class Process
{
    public int ProcessId { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;
    public int Order { get; set; }
    public int TimeInSeconds { get; set; }
    [Required] public List<string> Ingredients { get; set; } = new List<string>();
}