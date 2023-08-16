using GlobeWander.Data;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{
    public class TourSpotService : ITourSpot
    {
        private readonly GlobeWanderDbContext _context;

        public TourSpotService(GlobeWanderDbContext context)
        {
            _context = context;
        }
        public async Task<TourSpot> CreateTourSpot(TourSpot tourSpot)
        {
            _context.TourSpots.Add(tourSpot);

            await _context.SaveChangesAsync();

            return tourSpot;
        }

        public async Task DeleteTourSpot(int id)
        {
            var tourSpotToDelete = await _context.TourSpots.FindAsync(id);

            if (tourSpotToDelete != null)
            {
                _context.Entry<TourSpot>(tourSpotToDelete).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TourSpot>> GetAllTourSpots()
        {
            var allTourSpots = await _context.TourSpots.ToListAsync();

            return allTourSpots;
        }

        public async Task<TourSpot> GetSpotById(int id)
        {
            return await _context.TourSpots.FindAsync(id);
        }

        public async Task<TourSpot> UpdateTourSpot(TourSpot tourSpot, int id)
        {
            tourSpot.ID = id;

            _context.Entry<TourSpot>(tourSpot).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return tourSpot;
        }
    }
}
