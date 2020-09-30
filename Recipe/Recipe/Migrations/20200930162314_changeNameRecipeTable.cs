using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipe.Migrations
{
    public partial class changeNameRecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__Recip__286302EC",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK__Steps__RecipeId__2B3F6F97",
                table: "Steps");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Steps_RecipeId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "RecipeTableId",
                table: "Steps",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeTableId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeTables",
                columns: table => new
                {
                    RecipeTableId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RecipeName = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    RecipeDescription = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recipes__FDD988B02F32FFD7", x => x.RecipeTableId);
                    table.ForeignKey(
                        name: "FK__Recipes__UserId__25869641",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RecipeTableId",
                table: "Steps",
                column: "RecipeTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeTableId",
                table: "Ingredients",
                column: "RecipeTableId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTables_UserId",
                table: "RecipeTables",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__Recip__286302EC",
                table: "Ingredients",
                column: "RecipeTableId",
                principalTable: "RecipeTables",
                principalColumn: "RecipeTableId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Steps__RecipeId__2B3F6F97",
                table: "Steps",
                column: "RecipeTableId",
                principalTable: "RecipeTables",
                principalColumn: "RecipeTableId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Ingredien__Recip__286302EC",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK__Steps__RecipeId__2B3F6F97",
                table: "Steps");

            migrationBuilder.DropTable(
                name: "RecipeTables");

            migrationBuilder.DropIndex(
                name: "IX_Steps_RecipeTableId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeTableId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipeTableId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "RecipeTableId",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Steps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RecipeName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recipes__FDD988B02F32FFD7", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK__Recipes__UserId__25869641",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RecipeId",
                table: "Steps",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Ingredien__Recip__286302EC",
                table: "Ingredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Steps__RecipeId__2B3F6F97",
                table: "Steps",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
