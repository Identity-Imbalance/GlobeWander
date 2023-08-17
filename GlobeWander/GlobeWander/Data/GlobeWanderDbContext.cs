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

            modelBuilder.Entity<HotelRoom>()
                .HasOne(b => b.BookingRoom)  // BookingRoom references HotelRoom
                .WithOne(h => h.HotelRooms) // HotelRoom references BookingRoom
                .HasForeignKey<BookingRoom>(h => new { h.HotelID, h.RoomNumber })
                .OnDelete(DeleteBehavior.NoAction); 


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
