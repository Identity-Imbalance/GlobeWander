using GlobeWander.Data;
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
        
       
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}