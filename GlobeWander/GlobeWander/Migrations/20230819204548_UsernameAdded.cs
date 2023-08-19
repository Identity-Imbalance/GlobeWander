using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class UsernameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "BookingRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "BookingRooms");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 18, 16, 32, 153, DateTimeKind.Utc).AddTicks(3006), new DateTime(2023, 8, 19, 21, 16, 32, 153, DateTimeKind.Local).AddTicks(2994) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 18, 16, 32, 153, DateTimeKind.Utc).AddTicks(3010), new DateTime(2023, 8, 19, 21, 16, 32, 153, DateTimeKind.Local).AddTicks(3009) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 18, 16, 32, 153, DateTimeKind.Utc).AddTicks(3012), new DateTime(2023, 8, 19, 21, 16, 32, 153, DateTimeKind.Local).AddTicks(3011) });
        }
    }
}
