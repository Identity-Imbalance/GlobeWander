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

            modelBuilder.Entity<TourSpot>().HasData(
                new TourSpot() { ID = 1, Name = "Petra", Country = "Jordan", City = "Petra", Description = "a place before thousands years", Category = Category.Historical, PhoneNumber = 078885423 },
                new TourSpot() { ID = 2, Name = "Jerash", Country = "Jordan", City = "Jerash", Description = "A historical place that the Romanian civilization build before thousands years.", Category = Category.Historical, PhoneNumber = 088782215 },
                new TourSpot() { ID = 3, Name = "Um Qais", Country = "Jordan", City = "Irbid", Description = "A historical place that the Romanian civilization build before thousands years. In the north of Jordan", Category = Category.Historical, PhoneNumber = 0788442523 }
                );
            modelBuilder.Entity<Trip>().HasData(

                new Trip() { Id = 1, TourSpotID = 1, Name = "Petra ride", Cost = 20, Theme = "Discovering", Activity = "walking", StartDate = DateTime.Now, EndDate = DateTime.UtcNow, Description = "trip start at 8 am and going from Amman to Petra" },
                new Trip() { Id = 2, TourSpotID = 2, Name = "Jerash ride", Cost = 30, Theme = "Discovering", Activity = "visiting", StartDate = DateTime.Now, EndDate = DateTime.UtcNow, Description = "Amman to Jerash with a trip manager who can speak many languages" },
                new Trip() { Id = 3, TourSpotID = 3, Name = "Um-Qais ride", Cost = 40, Theme = "Discovering", Activity = "climbing", StartDate = DateTime.Now, EndDate = DateTime.UtcNow, Description = "Amman to Irbid with a trip manager who can speak many languages" }
            );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel() { Id = 1, TourSpotID = 1, Name = "Paradise", Description = "A unique hotel that you can't find in this place" },
                new Hotel() { Id = 2, TourSpotID = 2, Name = "Wander ", Description = "A unique hotel that you can't find in this place" },
                new Hotel() { Id = 3, TourSpotID = 3, Name = "Amazing", Description = "A unique hotel that you can't find in this place" }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room()
                {
                    ID = 1,
                    Name = "Small Room",
                    Layout = Layout.OneBed
                },
                new Room()
                {
                    ID = 2,
                    Name = "Suite Room",
                    Layout = Layout.TwoBed
                }
                ,
                new Room()
                {
                    ID = 3,
                    Name = "Studio room",
                    Layout = Layout.Studio
                }
                );


            modelBuilder.Entity<HotelRoom>().HasKey(
                 hotelRooms => new
                 {
                     hotelRooms.HotelID,
                     hotelRooms.RoomNumber
                 }
                 );

            modelBuilder.Entity<HotelRoom>()
                .HasOne(b => b.BookingRoom)  // BookingRoom references HotelRoom
                .WithOne(h => h.HotelRooms) // HotelRoom references BookingRoom
                .HasForeignKey<BookingRoom>(h => new { h.HotelID, h.RoomNumber })
                .OnDelete(DeleteBehavior.NoAction);


            //modelBuilder.Entity<Rate>().HasKey(
            //    rate => new
            //    {
            //        rate.ID,
            //        rate.TripID
            //    });
            //modelBuilder.Entity<BookingTrip>().HasKey(
            //    bookingTrip => new
            //    {
            //        bookingTrip.ID,
            //        bookingTrip.TripID
            //    });

            ///seeding roles 
            seedRole(modelBuilder, "Admin Manager", "create", "update", "delete", "read");
            seedRole(modelBuilder, "Tour Manager", "create", "update", "delete", "read");
            seedRole(modelBuilder, "Trip Manager", "create", "update", "delete", "read");
            seedRole(modelBuilder, "Hotel Manager", "create", "update", "delete", "read");
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
            var roleClaim = permissions.Select(permissions => 
            new IdentityRoleClaim<string>
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
