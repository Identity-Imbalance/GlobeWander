using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class newYpdateBookingTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "bookingTrips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "bookingTrips",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "bookingTrips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 22, 45, 14, 277, DateTimeKind.Utc).AddTicks(8726), new DateTime(2023, 8, 20, 1, 45, 14, 277, DateTimeKind.Local).AddTicks(8716) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 22, 45, 14, 277, DateTimeKind.Utc).AddTicks(8730), new DateTime(2023, 8, 20, 1, 45, 14, 277, DateTimeKind.Local).AddTicks(8729) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 22, 45, 14, 277, DateTimeKind.Utc).AddTicks(8734), new DateTime(2023, 8, 20, 1, 45, 14, 277, DateTimeKind.Local).AddTicks(8733) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "bookingTrips");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "bookingTrips");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "bookingTrips",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 20, 45, 48, 342, DateTimeKind.Utc).AddTicks(7879), new DateTime(2023, 8, 19, 23, 45, 48, 342, DateTimeKind.Local).AddTicks(7868) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 20, 45, 48, 342, DateTimeKind.Utc).AddTicks(7883), new DateTime(2023, 8, 19, 23, 45, 48, 342, DateTimeKind.Local).AddTicks(7882) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 20, 45, 48, 342, DateTimeKind.Utc).AddTicks(7885), new DateTime(2023, 8, 19, 23, 45, 48, 342, DateTimeKind.Local).AddTicks(7885) });
        }
    }
}
