using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelongeaBoulangerie.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class recipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Breads");

            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "Breads",
                newName: "BreadRecipe");

            migrationBuilder.CreateTable(
                name: "BreadUser",
                columns: table => new
                {
                    BreadsBreadId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreadUser", x => new { x.BreadsBreadId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_BreadUser_Breads_BreadsBreadId",
                        column: x => x.BreadsBreadId,
                        principalTable: "Breads",
                        principalColumn: "BreadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BreadUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreadUser_UsersUserId",
                table: "BreadUser",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreadUser");

            migrationBuilder.RenameColumn(
                name: "BreadRecipe",
                table: "Breads",
                newName: "Instructions");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Breads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
