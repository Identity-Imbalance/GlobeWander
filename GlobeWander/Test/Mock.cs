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

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}