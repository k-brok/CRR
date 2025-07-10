using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRR.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Trips");
        }
    }
}
