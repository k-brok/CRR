using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRR.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPrivateMilage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance_Business",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Distance_Private",
                table: "Trips",
                newName: "PrivateMileage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrivateMileage",
                table: "Trips",
                newName: "Distance_Private");

            migrationBuilder.AddColumn<int>(
                name: "Distance_Business",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
