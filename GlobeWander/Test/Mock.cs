using GlobeWander.Data;
using GlobeWander.Models;
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

        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel() { 
                Name = "Test", 
                Description = "Test", 
                TourSpotID = 1 };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, hotel.Id);

            return hotel;
        }


        protected async Task<Trip> CreateAndSaveTestTrip()
        {
            var trip = new Trip() { 
                Name = "Test", 
                Description = "Test", 
                Cost = 1.0, 
                Activity = "Test",
                StartDate = DateTime.Parse("2020-01-01"),
                EndDate = DateTime.Parse("2020-02-02"),
                Theme = "Test",
                TourSpotID = 2
            };
            _db.Trips.Add(trip);
            await _db.SaveChangesAsync();

            Assert.NotEqual(0, trip.Id);

            return trip;
        }


        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}