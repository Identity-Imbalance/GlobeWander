using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class seedingusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "f25f97e3-e90c-4cbe-af59-f435ab291251", "adminUser@example.com", true, false, null, "adminUser@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEE2uIRPYVNo9Ifw5AgC5zNpVHH+hY1nMfOBv+x6hMbL4R15319Kj23GjIC5MeZibTw==", "1234567890", false, "2c0bd26e-6ed0-436d-b0e2-04504486a15b", false, "admin" },
                    { "2", 0, "511e6655-3a1b-4950-9c08-ae17020e32f6", "hotel@example.com", true, false, null, "hotel@EXAMPLE.COM", "HOTEL", "AQAAAAIAAYagAAAAEOMHstQ2B4dCqbb2vBVQE7lCKI5Umc3Wqv/1wchpIPnRC7kr5ESyY0BN1jTOJM5zEg==", "1234567890", false, "27a42e39-73e5-4f07-bb5d-a122487e297d", false, "hotel" },
                    { "3", 0, "02b192a3-6996-452e-b52a-c382417fd060", "trip@example.com", true, false, null, "trip@EXAMPLE.COM", "TRIP", "AQAAAAIAAYagAAAAECi4ollBuEPeMoVaxAZGZNH2Pua9W/+03cpC7GzRy8JMRuN8uqsHH2EEQAXnW2DHTw==", "1234567890", false, "cfbfeef6-3b11-433a-a9ad-5fe8be6f373d", false, "trip" },
                    { "4", 0, "5f1d1bf3-3174-49e3-9840-28197ca6ee43", "User@example.com", true, false, null, null, "USER", "AQAAAAIAAYagAAAAEDyAYX3sm7I2GGETQrJZasfyfFpCwguGd08oCEjXK33taBZoE/34TXQ1/kNvW+bjYQ==", "1234567890", false, "c7263789-b4bb-410e-a0da-3fe897380077", false, "User" }
                });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 21, 21, 12, 43, 519, DateTimeKind.Utc).AddTicks(6857), new DateTime(2023, 8, 22, 0, 12, 43, 519, DateTimeKind.Local).AddTicks(6808) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 21, 21, 12, 43, 519, DateTimeKind.Utc).AddTicks(6862), new DateTime(2023, 8, 22, 0, 12, 43, 519, DateTimeKind.Local).AddTicks(6860) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 21, 21, 12, 43, 519, DateTimeKind.Utc).AddTicks(6865), new DateTime(2023, 8, 22, 0, 12, 43, 519, DateTimeKind.Local).AddTicks(6863) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "admin manager", "1" },
                    { "hotel manager", "2" },
                    { "trip manager", "3" },
                    { "user", "4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "admin manager", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "hotel manager", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "trip manager", "3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "user", "4" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 21, 19, 9, 49, 702, DateTimeKind.Utc).AddTicks(3984), new DateTime(2023, 8, 21, 22, 9, 49, 702, DateTimeKind.Local).AddTicks(3946) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 21, 19, 9, 49, 702, DateTimeKind.Utc).AddTicks(3988), new DateTime(2023, 8, 21, 22, 9, 49, 702, DateTimeKind.Local).AddTicks(3986) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 21, 19, 9, 49, 702, DateTimeKind.Utc).AddTicks(3991), new DateTime(2023, 8, 21, 22, 9, 49, 702, DateTimeKind.Local).AddTicks(3989) });
        }
    }
}
