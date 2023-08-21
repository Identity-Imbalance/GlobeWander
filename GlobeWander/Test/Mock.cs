using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.DTO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly GlobeWanderDbContext _db;
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


        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}