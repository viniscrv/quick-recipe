using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quick_recipe.Migrations
{
    public partial class add_recipe_in_progress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeInProgressId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeInProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrentStep = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Finished = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeInProgresses_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RecipeInProgressId",
                table: "Users",
                column: "RecipeInProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInProgresses_RecipeId",
                table: "RecipeInProgresses",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RecipeInProgresses_RecipeInProgressId",
                table: "Users",
                column: "RecipeInProgressId",
                principalTable: "RecipeInProgresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RecipeInProgresses_RecipeInProgressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RecipeInProgresses");

            migrationBuilder.DropIndex(
                name: "IX_Users_RecipeInProgressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RecipeInProgressId",
                table: "Users");
        }
    }
}
