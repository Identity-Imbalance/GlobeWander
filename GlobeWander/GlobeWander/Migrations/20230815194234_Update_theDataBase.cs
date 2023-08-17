using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobeWander.Migrations
{
    /// <inheritdoc />
    public partial class Update_theDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_BookingRooms_HotelID_RoomNumber",
                table: "HotelRooms");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BookingRooms_HotelID_RoomNumber",
                table: "BookingRooms");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRooms_HotelID_RoomNumber",
                table: "BookingRooms",
                columns: new[] { "HotelID", "RoomNumber" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingRooms_HotelRooms_HotelID_RoomNumber",
                table: "BookingRooms",
                columns: new[] { "HotelID", "RoomNumber" },
                principalTable: "HotelRooms",
                principalColumns: new[] { "HotelID", "RoomNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingRooms_HotelRooms_HotelID_RoomNumber",
                table: "BookingRooms");

            migrationBuilder.DropIndex(
                name: "IX_BookingRooms_HotelID_RoomNumber",
                table: "BookingRooms");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BookingRooms_HotelID_RoomNumber",
                table: "BookingRooms",
                columns: new[] { "HotelID", "RoomNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_BookingRooms_HotelID_RoomNumber",
                table: "HotelRooms",
                columns: new[] { "HotelID", "RoomNumber" },
                principalTable: "BookingRooms",
                principalColumns: new[] { "HotelID", "RoomNumber" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
