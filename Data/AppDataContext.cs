using Microsoft.EntityFrameworkCore;
using quick_recipe.Models;

namespace quick_recipe.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<Recipe> Recipes { get; set; } = null!;
    public DbSet<Process> Processes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<User>(entity =>
        // {
        //     entity.HasKey(u => u.Id);

        //     entity.HasMany(u => u.Recipes)
        //         .WithOne(r => r.User)
        //         .HasForeignKey(r => r.UserId);

        //     entity.HasMany(u => u.Menus)
        //         .WithOne(m => m.User)
        //         .HasForeignKey(m => m.UserId);
        // });

        // modelBuilder.Entity<Menu>(entity =>
        // {
        //     entity.HasKey(m => m.Id);

        //     entity.HasOne(m => m.User)
        //         .WithMany(u => u.Menus)
        //         .HasForeignKey(m => m.UserId);

        //     entity.HasMany(m => m.Recipes)
        //         .WithOne(r => r.Menu)
        //         .HasForeignKey(r => r.MenuId);
        // });

        // modelBuilder.Entity<Recipe>(entity =>
        // {
        //     entity.HasKey(r => r.Id);

        //     entity.HasOne(r => r.User)
        //         .WithMany(u => u.Recipes)
        //         .HasForeignKey(r => r.UserId);

        //     entity.HasOne(r => r.Menu)
        //         .WithMany(m => m.Recipes)
        //         .HasForeignKey(r => r.MenuId);

        //     entity.HasMany(r => r.Processes)
        //         .WithOne(p => p.Recipe)
        //         .HasForeignKey(p => p.RecipeId);
        // });

        // modelBuilder.Entity<Process>(entity =>
        // {
        //     entity.HasKey(p => p.Id);

        //     entity.HasOne(p => p.Recipe)
        //         .WithMany(r => r.Processes)
        //         .HasForeignKey(p => p.RecipeId);
        // });
    }
}