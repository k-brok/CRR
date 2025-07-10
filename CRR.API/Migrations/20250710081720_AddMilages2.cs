using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRR.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMilages2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArrivalMileage",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartureMileage",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalMileage",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DepartureMileage",
                table: "Trips");
        }
    }
}
