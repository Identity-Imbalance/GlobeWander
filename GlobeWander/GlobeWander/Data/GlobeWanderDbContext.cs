using GlobeWander.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Data
{
    public class GlobeWanderDbContext : DbContext
    {
        public GlobeWanderDbContext(DbContextOptions option) : base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelRoom>().HasKey(
                hotelRooms => new {
                    hotelRooms.HotelID,
                    hotelRooms.RoomNumber
                }
                );

            modelBuilder.Entity<BookingRoom>()
                .HasOne(b => b.HotelRooms)  // BookingRoom references HotelRoom
                .WithOne(h => h.BookingRoom) // HotelRoom references BookingRoom
                .HasForeignKey<HotelRoom>(h => new { h.HotelID, h.RoomNumber }) // Define the composite foreign key
                .HasPrincipalKey<BookingRoom>(b => new { b.HotelID, b.RoomNumber });


            modelBuilder.Entity<Rate>().HasKey(
                rate => new
                {
                    rate.ID,
                    rate.TripID
                });
            modelBuilder.Entity<BookingTrip>().HasKey(
                bookingTrip => new
                {
                    bookingTrip.ID,
                    bookingTrip.TripID
                });

            
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<TourSpot> TourSpots { get; set;}

        public DbSet<Trip> Trips { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<BookingRoom> BookingRooms { get; set; }

        public DbSet<BookingTrip>  bookingTrips { get; set; }



    }
}
