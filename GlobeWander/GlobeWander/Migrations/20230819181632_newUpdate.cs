using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class newUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "HotelRooms",
                newName: "PricePerDay");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "BookingRooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "BookingRooms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "BookingRooms");

            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "HotelRooms",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "BookingRooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 16, 34, 47, 644, DateTimeKind.Utc).AddTicks(4641), new DateTime(2023, 8, 19, 19, 34, 47, 644, DateTimeKind.Local).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 16, 34, 47, 644, DateTimeKind.Utc).AddTicks(4644), new DateTime(2023, 8, 19, 19, 34, 47, 644, DateTimeKind.Local).AddTicks(4643) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 19, 16, 34, 47, 644, DateTimeKind.Utc).AddTicks(4647), new DateTime(2023, 8, 19, 19, 34, 47, 644, DateTimeKind.Local).AddTicks(4646) });
        }
    }
}
