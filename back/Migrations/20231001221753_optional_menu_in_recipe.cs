using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quick_recipe.Migrations
{
    public partial class optional_menu_in_recipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Menus_MenuId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "Recipes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Menus_MenuId",
                table: "Recipes",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Menus_MenuId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Menus_MenuId",
                table: "Recipes",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
