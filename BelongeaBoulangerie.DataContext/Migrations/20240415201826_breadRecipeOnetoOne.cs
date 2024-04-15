using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelongeaBoulangerie.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class breadRecipeOnetoOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreadRecipe",
                table: "Breads");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    Grams = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    InstructionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructionString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.InstructionId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BakeTime = table.Column<int>(type: "int", nullable: false),
                    BreadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipes_Breads_BreadId",
                        column: x => x.BreadId,
                        principalTable: "Breads",
                        principalColumn: "BreadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    IngredientsIngredientId = table.Column<int>(type: "int", nullable: false),
                    RecipesRecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => new { x.IngredientsIngredientId, x.RecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredients_IngredientsIngredientId",
                        column: x => x.IngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructionRecipe",
                columns: table => new
                {
                    InstructionsInstructionId = table.Column<int>(type: "int", nullable: false),
                    RecipesRecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionRecipe", x => new { x.InstructionsInstructionId, x.RecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_InstructionRecipe_Instructions_InstructionsInstructionId",
                        column: x => x.InstructionsInstructionId,
                        principalTable: "Instructions",
                        principalColumn: "InstructionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructionRecipe_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_RecipesRecipeId",
                table: "IngredientRecipe",
                column: "RecipesRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionRecipe_RecipesRecipeId",
                table: "InstructionRecipe",
                column: "RecipesRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_BreadId",
                table: "Recipes",
                column: "BreadId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.DropTable(
                name: "InstructionRecipe");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "BreadRecipe",
                table: "Breads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
