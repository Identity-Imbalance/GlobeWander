using GlobeWander.Data;
using GlobeWander.Models.DTO;
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

        public async Task<TripDTO> CreateTrip(NewTripDTO trip)
        {
            Trip newTrip = new Trip()
            {
                Name = trip.Name,
                Cost = trip.Cost,
                Activity = trip.Activity,
                Description = trip.Description,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Theme = trip.Theme,
                TourSpotID = trip.TourSpotID
                
                
            };
            _context.Entry(newTrip).State = EntityState.Added;

            await _context.SaveChangesAsync();

            TripDTO returnedTrip = await GetTripByID(newTrip.Id);

            returnedTrip.Id = newTrip.Id;
            trip.Id = newTrip.Id;

            return returnedTrip;
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

        public async Task<List<TripDTO>> GetAllTrips()
        {
            return await _context.Trips.Select(
                tr=> new TripDTO
                {
                    Id = tr.Id,
                    Name = tr.Name,
                    Description = tr.Description,
                    Cost = tr.Cost,
                    Activity = tr.Activity,
                    StartDate = tr.StartDate,
                    EndDate = tr.EndDate,
                    Theme = tr.Theme,
                    TourSpotID = tr.TourSpotID,
                    BookingTrips = tr.BookingTrips.Select(bt => new BookingTripDTO
                    {
                        ID = bt.ID,
                        TripID  = bt.TripID,
                        NumberOfPersons = bt.NumberOfPersons,
                        CostPerPerson = bt.CostPerPerson,
                        Duration = bt.Duration
                    }).ToList(),
                    Rates = tr.Rates.Select(r=> new RateDTO
                    {
                        ID=r.ID,
                        TripID=r.TripID,
                        Comments = r.Comments,
                        Rating = r.Rating
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<TripDTO> GetTripByID(int id)
        {
            TripDTO? trip = await _context.Trips
                .Where(x=>x.Id == id)
                .Select(
                tr => new TripDTO
                {
                    Id = tr.Id,
                    Name = tr.Name,
                    Description = tr.Description,
                    Cost = tr.Cost,
                    Activity = tr.Activity,
                    StartDate = tr.StartDate,
                    EndDate = tr.EndDate,
                    Theme = tr.Theme,
                    TourSpotID = tr.TourSpotID,
                    BookingTrips = tr.BookingTrips.Select(bt => new BookingTripDTO
                    {
                        ID = bt.ID,
                        TripID = bt.TripID,
                        NumberOfPersons = bt.NumberOfPersons,
                        CostPerPerson = bt.CostPerPerson,
                        Duration = bt.Duration
                    }).ToList(),
                    Rates = tr.Rates.Select(r => new RateDTO
                    {
                        ID = r.ID,
                        TripID = r.TripID,
                        Comments = r.Comments,
                        Rating = r.Rating
                    }).ToList()
                }).FirstOrDefaultAsync();
            return trip;
        }

        public async Task<TripDTO> UpdateTrip(NewTripDTO trip, int id)
        {
           var updateTrip = await _context.Trips.FindAsync(id);

            if (updateTrip != null) 
            {
                trip.Id = updateTrip.Id;
                updateTrip.Name = trip.Name;
                updateTrip.Description = trip.Description;
                updateTrip.StartDate = trip.StartDate;
                updateTrip.Cost = trip.Cost;
                updateTrip.EndDate = trip.EndDate;
                updateTrip.Activity = trip.Activity;
                updateTrip.Theme = trip.Theme;

                _context.Entry(updateTrip).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            var returnedTrip = await GetTripByID(id);

            return returnedTrip;

        }
    }
}
