using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelongeaBoulangerie.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class usersupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_EmailAddress",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "BakeTime",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_EmailAddress",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "BakeTime",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_EmailAddress",
                table: "Users",
                column: "EmailAddress");
        }
    }
}
