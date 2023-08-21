using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class newRateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Rates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 20, 21, 9, 59, 120, DateTimeKind.Utc).AddTicks(4014), new DateTime(2023, 8, 21, 0, 9, 59, 120, DateTimeKind.Local).AddTicks(4002) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 20, 21, 9, 59, 120, DateTimeKind.Utc).AddTicks(4017), new DateTime(2023, 8, 21, 0, 9, 59, 120, DateTimeKind.Local).AddTicks(4016) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 20, 21, 9, 59, 120, DateTimeKind.Utc).AddTicks(4020), new DateTime(2023, 8, 21, 0, 9, 59, 120, DateTimeKind.Local).AddTicks(4019) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Rates");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 20, 10, 42, 14, 806, DateTimeKind.Utc).AddTicks(843), new DateTime(2023, 8, 20, 13, 42, 14, 806, DateTimeKind.Local).AddTicks(831) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 20, 10, 42, 14, 806, DateTimeKind.Utc).AddTicks(847), new DateTime(2023, 8, 20, 13, 42, 14, 806, DateTimeKind.Local).AddTicks(846) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 20, 10, 42, 14, 806, DateTimeKind.Utc).AddTicks(850), new DateTime(2023, 8, 20, 13, 42, 14, 806, DateTimeKind.Local).AddTicks(849) });
        }
    }
}
