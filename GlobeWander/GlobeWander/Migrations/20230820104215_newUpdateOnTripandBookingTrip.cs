using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class newUpdateOnTripandBookingTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Trips",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "Cost", "Count", "EndDate", "StartDate" },
                values: new object[] { 0, 20m, 0, new DateTime(2023, 8, 20, 10, 42, 14, 806, DateTimeKind.Utc).AddTicks(843), new DateTime(2023, 8, 20, 13, 42, 14, 806, DateTimeKind.Local).AddTicks(831) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "Cost", "Count", "EndDate", "StartDate" },
                values: new object[] { 0, 30m, 0, new DateTime(2023, 8, 20, 10, 42, 14, 806, DateTimeKind.Utc).AddTicks(847), new DateTime(2023, 8, 20, 13, 42, 14, 806, DateTimeKind.Local).AddTicks(846) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Capacity", "Cost", "Count", "EndDate", "StartDate" },
                values: new object[] { 0, 40m, 0, new DateTime(2023, 8, 20, 10, 42, 14, 806, DateTimeKind.Utc).AddTicks(850), new DateTime(2023, 8, 20, 13, 42, 14, 806, DateTimeKind.Local).AddTicks(849) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Trips");

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Trips",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cost", "EndDate", "StartDate" },
                values: new object[] { 20.0, new DateTime(2023, 8, 19, 22, 45, 14, 277, DateTimeKind.Utc).AddTicks(8726), new DateTime(2023, 8, 20, 1, 45, 14, 277, DateTimeKind.Local).AddTicks(8716) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cost", "EndDate", "StartDate" },
                values: new object[] { 30.0, new DateTime(2023, 8, 19, 22, 45, 14, 277, DateTimeKind.Utc).AddTicks(8730), new DateTime(2023, 8, 20, 1, 45, 14, 277, DateTimeKind.Local).AddTicks(8729) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Cost", "EndDate", "StartDate" },
                values: new object[] { 40.0, new DateTime(2023, 8, 19, 22, 45, 14, 277, DateTimeKind.Utc).AddTicks(8734), new DateTime(2023, 8, 20, 1, 45, 14, 277, DateTimeKind.Local).AddTicks(8733) });
        }
    }
}
