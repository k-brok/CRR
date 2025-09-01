using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRR.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultTripTime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TripTime",
                table: "DefaultTrips",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripTime",
                table: "DefaultTrips");
        }
    }
}
