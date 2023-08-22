using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly GlobeWanderDbContext _db;
        protected readonly UserManager<ApplicationUser> _UserManager;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new GlobeWanderDbContext(
                  new DbContextOptionsBuilder<GlobeWanderDbContext>()
                          .UseSqlite(_connection).Options);
            _db.Database.EnsureCreated();

        }
        //Write Your Code Here....
        protected async Task<Rate> CreateRatesAndSave()
        {
            var rate = new Rate()
            {
                ID = 1,
                TripID = 1,
                Comments = "Test Comment",
                Rating = "4"
            };
            _db.Rates.Add(rate);
            await _db.SaveChangesAsync();
            return rate;
        }
        protected async Task<Trip> CreateTripAndSave()
        {
            var trip = new Trip()
            {
                Name = "Test",
                Description = "Test",
                Cost = 2,
                Activity = "NoActive",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Theme = "shaker",
                Capacity = 20,
                Count = 1,
                TourSpotID = 1,

            };
            _db.Trips.Add(trip);
            await _db.SaveChangesAsync();
            return trip;

        }

        protected async Task<BookingTrip> CreateBookTrip()
        {
            var bookingTrip = new BookingTrip()
            {
                //ID = 1,
                TripID = 1,
                NumberOfPersons = 2,
                CostPerPerson =25,
                Duration=4,
                TotalPrice = 50,
                Username="mdht"
            };
            _db.bookingTrips.Add(bookingTrip);
            await _db.SaveChangesAsync();
            return bookingTrip;
            }

        protected DbContextOptions<GlobeWanderDbContext> CreateInMemoryDatabaseOptions()
        {
            return new DbContextOptionsBuilder<GlobeWanderDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .EnableSensitiveDataLogging() // Add this line to enable sensitive data logging
                .Options;
        }
        public static UserManager<TUser> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var userManagerMock = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);

            return userManagerMock.Object;
        }

      

        protected async Task<TourSpot> CreateAndSaveTestTourSpot()
        {
            var tourSpot = new TourSpot()
            {
                Name = "Test",
                Country = "Test",
                City = "Test",
                Description = "Test",
                Category = Category.Historical,
                PhoneNumber = 24343
            };

            _db.TourSpots.Add(tourSpot);
            await _db.SaveChangesAsync();


            return tourSpot;


        }

        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel() { Name = "Test", Description = "Test", TourSpotID = 1 };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, hotel.Id);

            return hotel;
        }
                
        protected async  Task<Room> CreateandSaveRoom ()
        {
            var room = new Room() {Name ="Room3" , Layout = Layout.OneBed };
             _db.Add(room);
            await _db.SaveChangesAsync();

                    return room;
        }
        protected async Task<HotelRoom> CreateandSaveHotelRoom()
        {
            var HotelRooms = new HotelRoom() {RoomNumber=5, PricePerDay =100 , IsAvailable=true};
            _db.Add(HotelRooms);
            await _db.SaveChangesAsync();

            return HotelRooms;
        }

        protected static IUser SetupUserMock(UserDTO expectedResult)
        {
            var userMock = new Mock<IUser>();

            userMock.Setup(u => u.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(expectedResult);

            return userMock.Object;
        }
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
            _UserManager?.Dispose();
            _UserManager?.Dispose();
        }

        protected async Task<HotelRoom> CreateHotelRoom()
        {
            var hotelRoom = new HotelRoom()
            {
                
                RoomNumber = 202,
                HotelID = 1,
                RoomID = 1,
                PricePerDay = 200,
                IsAvailable = true,

            };
            _db.HotelRooms.Add(hotelRoom);
            await _db.SaveChangesAsync();
            return hotelRoom;
        }

       
    }
}