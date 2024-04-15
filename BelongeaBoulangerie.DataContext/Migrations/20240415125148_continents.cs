using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelongeaBoulangerie.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class continents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryContinent",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "UnAssigned");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryContinent",
                table: "Countries");
        }
    }
}
