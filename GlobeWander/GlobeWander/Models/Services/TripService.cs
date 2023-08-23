using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{/// <summary>
/// Service for managing trips.
/// </summary>
    public class TripService : ITrip
    {
        private readonly GlobeWanderDbContext _context;

        public TripService(GlobeWanderDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new trip.
        /// </summary>
        /// <param name="trip">Trip data.</param>

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
                Capacity = trip.Capacity,
                Count = trip.Count,
                TourSpotID = trip.TourSpotID
                
            };
            _context.Entry(newTrip).State = EntityState.Added;

            await _context.SaveChangesAsync();

            TripDTO returnedTrip = await GetTripByID(newTrip.Id);

            returnedTrip.Id = newTrip.Id;
            trip.Id = newTrip.Id;

            return returnedTrip;
        }

        /// <summary>
        /// Delete a trip by ID.
        /// </summary>
        /// <param name="id">ID of the trip to be deleted.</param>
        public async Task DeleteTrip(int id)
        {
            var tripId = await _context.Trips.FindAsync(id);

            if (tripId != null)
            {
                _context.Entry<Trip>(tripId).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a list of all trips.
        /// </summary>
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
                    Capacity = tr.Capacity,
                    Count = tr.Count,
                    TourSpotID = tr.TourSpotID,
                    BookingTrips = tr.BookingTrips.Select(bt => new BookingTripDTO
                    {
                        ID = bt.ID,
                        TripID  = bt.TripID,
                        NumberOfPersons = bt.NumberOfPersons,
                        CostPerPerson = bt.CostPerPerson,
                        Duration = bt.Duration,
                        TotalPrice = bt.TotalPrice,
                        Username = bt.Username

                    }).ToList(),
                    Rates = tr.Rates.Select(r=> new RateDTO
                    {
                        ID=r.ID,
                        TripID=r.TripID,
                        Comments = r.Comments,
                        Rating = r.Rating,
                        Username =r.Username
                    }).ToList()
                }).ToListAsync();
        }

        /// <summary>
        /// Get trip data by ID.
        /// </summary>
        /// <param name="id">ID of the trip.</param>
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
                    Capacity = tr.Capacity,
                    Count = tr.Count,
                    TourSpotID = tr.TourSpotID,
                    BookingTrips = tr.BookingTrips.Select(bt => new BookingTripDTO
                    {
                        ID = bt.ID,
                        TripID = bt.TripID,
                        NumberOfPersons = bt.NumberOfPersons,
                        CostPerPerson = bt.CostPerPerson,
                        Duration = bt.Duration,
                        TotalPrice = bt.TotalPrice,
                        Username = bt.Username
                    }).ToList(),
                    Rates = tr.Rates.Select(r => new RateDTO
                    {
                        ID = r.ID,
                        TripID = r.TripID,
                        Comments = r.Comments,
                        Rating = r.Rating,
                        Username = r.Username
                    }).ToList()
                }).FirstOrDefaultAsync();
            return trip;
        }

        /// <summary>
        /// Update trip data by ID.
        /// </summary>
        /// <param name="trip">Updated trip data.</param>
        /// <param name="id">ID of the trip to be updated.</param>
        public async Task<TripDTO> UpdateTrip(UpdateTripDTO trip, int id)
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

                _context.Entry(updateTrip).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            var returnedTrip = await GetTripByID(id);

            return returnedTrip;

        }
    }
}
