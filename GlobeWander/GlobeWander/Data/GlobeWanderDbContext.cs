using GlobeWander.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Data
{
    public class GlobeWanderDbContext : IdentityDbContext<ApplicationUser>
    {
        public GlobeWanderDbContext(DbContextOptions option) : base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);

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
            ///seeding roles 
            seedRole(modelBuilder, "Admin Manager","create","update","delete", "read");
            seedRole(modelBuilder, "Tour Manager", "create", "update", "delete", "read");
            seedRole(modelBuilder, "Trip Manager", "create", "update", "delete", "read");
            seedRole(modelBuilder, "Hotel Manager","create", "update", "delete", "read");
            seedRole(modelBuilder, "User", "create", "update", "delete", "read");
            seedRole(modelBuilder, "AnonymousUser", "read");
        }

            int nextId = 1;
        private void seedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            var roleClaim = permissions.Select(permissions => new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType="permissions",
                    ClaimValue = permissions
            }).ToArray();
            modelBuilder.Entity<IdentityRole>().HasData(role);

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
