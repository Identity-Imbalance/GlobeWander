using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdateOnTripAndRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Rates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "EndDate", "StartDate" },
                values: new object[] { 30, new DateTime(2023, 8, 21, 8, 51, 22, 707, DateTimeKind.Utc).AddTicks(4072), new DateTime(2023, 8, 21, 11, 51, 22, 707, DateTimeKind.Local).AddTicks(4049) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "EndDate", "StartDate" },
                values: new object[] { 22, new DateTime(2023, 8, 21, 8, 51, 22, 707, DateTimeKind.Utc).AddTicks(4075), new DateTime(2023, 8, 21, 11, 51, 22, 707, DateTimeKind.Local).AddTicks(4074) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Capacity", "EndDate", "StartDate" },
                values: new object[] { 40, new DateTime(2023, 8, 21, 8, 51, 22, 707, DateTimeKind.Utc).AddTicks(4078), new DateTime(2023, 8, 21, 11, 51, 22, 707, DateTimeKind.Local).AddTicks(4077) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Rates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "EndDate", "StartDate", "Theme" },
                values: new object[] { 0, new DateTime(2023, 8, 20, 21, 9, 59, 120, DateTimeKind.Utc).AddTicks(4014), new DateTime(2023, 8, 21, 0, 9, 59, 120, DateTimeKind.Local).AddTicks(4002), "Discovering" });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "EndDate", "StartDate", "Theme" },
                values: new object[] { 0, new DateTime(2023, 8, 20, 21, 9, 59, 120, DateTimeKind.Utc).AddTicks(4017), new DateTime(2023, 8, 21, 0, 9, 59, 120, DateTimeKind.Local).AddTicks(4016), "Discovering" });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Capacity", "EndDate", "StartDate", "Theme" },
                values: new object[] { 0, new DateTime(2023, 8, 20, 21, 9, 59, 120, DateTimeKind.Utc).AddTicks(4020), new DateTime(2023, 8, 21, 0, 9, 59, 120, DateTimeKind.Local).AddTicks(4019), "Discovering" });
        }
    }
}
