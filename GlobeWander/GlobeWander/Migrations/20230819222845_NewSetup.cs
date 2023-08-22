using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class NewSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 22, 28, 44, 718, DateTimeKind.Utc).AddTicks(6968), new DateTime(2023, 8, 20, 1, 28, 44, 718, DateTimeKind.Local).AddTicks(6950) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 22, 28, 44, 718, DateTimeKind.Utc).AddTicks(6974), new DateTime(2023, 8, 20, 1, 28, 44, 718, DateTimeKind.Local).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 22, 28, 44, 718, DateTimeKind.Utc).AddTicks(6978), new DateTime(2023, 8, 20, 1, 28, 44, 718, DateTimeKind.Local).AddTicks(6977) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
