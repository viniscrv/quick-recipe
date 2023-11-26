namespace quick_recipe.Models;

public class RecipeInProgress
{
    public int Id { get; set; }
    public int CurrentStep { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public Boolean Finished { get; set; } = false;
    public DateTime StartedAt { get; } = DateTime.Now;
}