using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSeaArzu.Migrations
{
    /// <inheritdoc />
    public partial class AddItalianLanguageAndTripDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionIt",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DurationHours",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExcludesAr",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExcludesDe",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExcludesEn",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExcludesIt",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExcludesRo",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncludesAr",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncludesDe",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncludesEn",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncludesIt",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncludesRo",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationAr",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationDe",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationEn",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationIt",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationRo",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameIt",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBefore",
                table: "Trips",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionIt",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DurationHours",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExcludesAr",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExcludesDe",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExcludesEn",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExcludesIt",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExcludesRo",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IncludesAr",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IncludesDe",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IncludesEn",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IncludesIt",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "IncludesRo",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "LocationAr",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "LocationDe",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "LocationEn",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "LocationIt",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "LocationRo",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "NameIt",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PriceBefore",
                table: "Trips");
        }
    }
}
