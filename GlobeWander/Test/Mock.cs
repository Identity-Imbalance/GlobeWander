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
            _connection = new SqliteConnection("Filename=:memory");
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

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
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