using Microsoft.EntityFrameworkCore;
using quick_recipe.Models;

namespace quick_recipe.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Process> Processes { get; set; }
}