using GlobeWander.Data;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{
    public class TripService : ITrip
    {
        private readonly GlobeWanderDbContext _context;

        public TripService(GlobeWanderDbContext context)
        {
            _context = context;
        }

        public async Task<Trip> CreateTrip(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
            return trip;
        }

        public async Task DeleteTrip(int id)
        {
            var tripId = await _context.Trips.FindAsync(id);

            if (tripId != null)
            {
                _context.Entry<Trip>(tripId).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Trip>> GetAllTrips()
        {
            var allTrips =  await _context.Trips.ToListAsync();

            return allTrips;
        }

        public async Task<Trip> GetTripByID(int id)
        {
            Trip? trip = await _context.Trips.FindAsync(id);

            return trip;
        }

        public async Task<Trip> UpdateTrip(Trip trip, int id)
        {
            if (id == trip.Id)
            {
                _context.Entry(trip).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            return trip;

        }
    }
}
