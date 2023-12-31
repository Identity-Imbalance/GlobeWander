﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class NewSetupLastChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Layout = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TourSpots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourSpots", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourSpotID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_TourSpots_TourSpotID",
                        column: x => x.TourSpotID,
                        principalTable: "TourSpots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TourSpotID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_TourSpots_TourSpotID",
                        column: x => x.TourSpotID,
                        principalTable: "TourSpots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => new { x.HotelID, x.RoomNumber });
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRooms_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookingTrips",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    NumberOfPersons = table.Column<int>(type: "int", nullable: false),
                    CostPerPerson = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingTrips", x => x.ID);
                    table.ForeignKey(
                        name: "FK_bookingTrips_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rates_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingRooms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRooms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingRooms_HotelRooms_HotelID_RoomNumber",
                        columns: x => new { x.HotelID, x.RoomNumber },
                        principalTable: "HotelRooms",
                        principalColumns: new[] { "HotelID", "RoomNumber" });
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin manager", "00000000-0000-0000-0000-000000000000", "Admin Manager", "ADMIN MANAGER" },
                    { "hotel manager", "00000000-0000-0000-0000-000000000000", "Hotel Manager", "HOTEL MANAGER" },
                    { "trip manager", "00000000-0000-0000-0000-000000000000", "Trip Manager", "TRIP MANAGER" },
                    { "user", "00000000-0000-0000-0000-000000000000", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "477c5124-a3da-4d8e-8621-35799e4582d6", "adminUser@example.com", true, false, null, "adminUser@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAECa94HADwvhErWzvHWuast983NIo3+i2+L7e+nw58F95qVoTbycRdMColPbhlGCDDA==", "1234567890", false, "c6692053-4094-40f7-baad-513a2532b3dd", false, "admin" },
                    { "2", 0, "1c8a2b58-cb45-4665-8520-7c7cb6239926", "hotel@example.com", true, false, null, "hotel@EXAMPLE.COM", "HOTEL", "AQAAAAIAAYagAAAAEL22hOHvooTY7FGghzpoBR+6gdn1OnY+TetTfsAijgB/RJsbLO7Lr8+k+dkDTYETkw==", "1234567890", false, "12eb3f23-2c0c-4623-a2d1-88df2bfdf9ff", false, "hotel" },
                    { "3", 0, "f1eea056-7a23-47b3-844d-8e4e5c8f5eaa", "trip@example.com", true, false, null, "trip@EXAMPLE.COM", "TRIP", "AQAAAAIAAYagAAAAEH7EsDuhN7p4uEtiHJVduuTM6lbnWkcwBbJcxA8gfB2JJMwla0/IfDcJTXrT40dp9g==", "1234567890", false, "b6ae56e1-239c-469f-a396-583b7635475e", false, "trip" },
                    { "4", 0, "ac8607ba-a948-425d-a65b-38795f95351c", "User@example.com", true, false, null, null, "USER", "AQAAAAIAAYagAAAAEAoMx43nMY9GbINd5mV4+ZnsnKqDaQWiLMEKG9K29AB4C1OHJDKRTlFhFAEgTPJEpQ==", "1234567890", false, "82831347-82f9-480b-8301-90056c44c37b", false, "User" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "ID", "Layout", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Small Room" },
                    { 2, 3, "Suite Room" },
                    { 3, 1, "Studio room" }
                });

            migrationBuilder.InsertData(
                table: "TourSpots",
                columns: new[] { "ID", "Category", "City", "Country", "Description", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 3, "Petra", "Jordan", "a place before thousands years", "Petra", "078885423" },
                    { 2, 3, "Jerash", "Jordan", "A historical place that the Romanian civilization build before thousands years.", "Jerash", "088782215" },
                    { 3, 3, "Irbid", "Jordan", "A historical place that the Romanian civilization build before thousands years. In the north of Jordan", "Um Qais", "0788442521" }
                });

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

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Description", "Name", "TourSpotID" },
                values: new object[,]
                {
                    { 1, "A unique hotel that you can't find in this place", "Paradise", 1 },
                    { 2, "A unique hotel that you can't find in this place", "Wander ", 2 },
                    { 3, "A unique hotel that y    ou can't find in this place", "Amazing", 3 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "Activity", "Capacity", "Cost", "Count", "Description", "EndDate", "Name", "StartDate", "TourSpotID" },
                values: new object[,]
                {
                    { 1, "walking", 30, 20m, 0, "trip start at 8 am and going from Amman to Petra", new DateTime(2023, 8, 23, 21, 40, 37, 979, DateTimeKind.Utc).AddTicks(8154), "Petra ride", new DateTime(2023, 8, 24, 0, 40, 37, 979, DateTimeKind.Local).AddTicks(8139), 1 },
                    { 2, "visiting", 22, 30m, 0, "Amman to Jerash with a trip manager who can speak many languages", new DateTime(2023, 8, 23, 21, 40, 37, 979, DateTimeKind.Utc).AddTicks(8158), "Jerash ride", new DateTime(2023, 8, 24, 0, 40, 37, 979, DateTimeKind.Local).AddTicks(8157), 2 },
                    { 3, "climbing", 40, 40m, 0, "Amman to Irbid with a trip manager who can speak many languages", new DateTime(2023, 8, 23, 21, 40, 37, 979, DateTimeKind.Utc).AddTicks(8161), "Um-Qais ride", new DateTime(2023, 8, 24, 0, 40, 37, 979, DateTimeKind.Local).AddTicks(8160), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRooms_HotelID_RoomNumber",
                table: "BookingRooms",
                columns: new[] { "HotelID", "RoomNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookingTrips_TripID",
                table: "bookingTrips",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_RoomID",
                table: "HotelRooms",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_TourSpotID",
                table: "Hotels",
                column: "TourSpotID");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_TripID",
                table: "Rates",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TourSpotID",
                table: "Trips",
                column: "TourSpotID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookingRooms");

            migrationBuilder.DropTable(
                name: "bookingTrips");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "TourSpots");
        }
    }
}
