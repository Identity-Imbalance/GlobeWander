using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GlobeWander.Models.Services
{/// <summary>
/// Service for managing ratings and comments for trips.
/// </summary>
    public class RateService : IRate
    {
        private readonly GlobeWanderDbContext _rateService;

        private UserManager<ApplicationUser> _UserManager;


        public RateService(GlobeWanderDbContext rateService , UserManager<ApplicationUser> UserManager)
        {
            _rateService = rateService;
            _UserManager = UserManager;
        }


        /// <summary>
        /// Create a new rating and comment for a trip.
        /// </summary>
        /// <param name="rateDTO">Rating and comment data.</param>
        /// <param name="userPrincipal">User's claims principal.</param>
        public async Task<RateDTO> Create(NewRateDTO rateDTO, ClaimsPrincipal userPrincipal)
        {
            var getUserId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            var trip = await _rateService.Trips.FindAsync(rateDTO.TripID);

            var newUser = await _UserManager.FindByIdAsync(getUserId);

            var existBookingTrip = await _rateService.Rates
                                    .Where(x => x.TripID == rateDTO.TripID)
                                    .FirstOrDefaultAsync(b => b.Username == newUser.UserName && trip.Id == rateDTO.TripID);

            if (existBookingTrip == null)
            {
                var rate = new Rate
                {
                    TripID = rateDTO.TripID,
                    Comments = rateDTO.Comments,
                    Rating = rateDTO.Rating,
                    Username = newUser.UserName
                };
                _rateService.Rates.Add(rate);
                await _rateService.SaveChangesAsync();

                var newRate = await GetRateById(rate.ID);

                return newRate;
            }
            return null;

        }


        public Task DeleteAllRatesByTripID(int tripId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a rating and comment for a trip by ID.
        /// </summary>
        /// <param name="id">ID of the rating to be deleted.</param>
        /// <param name="TripId">ID of the trip.</param>
        public async Task<Rate> DeleteRate(int id ,int TripId)
        {
            var rate = await _rateService.Rates.Where(x => x.ID == id && x.TripID ==TripId).FirstOrDefaultAsync();
           // var rateDto = await GetRateById(id);
            _rateService.Rates.Remove(rate);
            await _rateService.SaveChangesAsync();
            return rate;
           
        }

        /// <summary>
        /// Get a list of all ratings and comments for trips.
        /// </summary>
        public async Task<List<RateDTO>> GetAllRate()
        {
            return await _rateService.Rates.Select(r => new RateDTO
            {
                ID = r.ID,
                TripID = r.TripID,
                Comments = r.Comments,
                Rating = r.Rating,
                Username = r.Username
            }
            ).ToListAsync();         
        }

        /// <summary>
        /// Get a rating and comment for a trip by ID.
        /// </summary>
        /// <param name="id">ID of the rating.</param>
        public async Task<RateDTO> GetRateById(int id)
        {
            var rate =  await _rateService.Rates.Where(x => x.ID == id)
                .Select(r => new RateDTO
            {
                ID = r.ID,
                TripID = r.TripID,
                Comments = r.Comments,
                Rating = r.Rating,
                Username = r.Username
            }
             ).FirstOrDefaultAsync();

            return rate;
        }

        /// <summary>
        /// Get a list of ratings and comments for a specific trip.
        /// </summary>
        /// <param name="tripId">ID of the trip.</param>
        public async Task<List<RateDTO>> GetRateByTripID(int tripId)
        {
            var rateByTripId = await _rateService.Rates.Where(
                rt=> rt.TripID == tripId)
                .Select(r => new RateDTO
                {
                    ID= r.ID,
                    TripID  = r.TripID,
                    Comments = r.Comments,
                    Rating = r.Rating,
                    Username = r.Username
                    
                }).ToListAsync();

            return rateByTripId;

        }

        /// <summary>
        /// Update a rating and comment for a trip.
        /// </summary>
        /// <param name="id">ID of the rating to be updated.</param>
        /// <param name="tripId">ID of the trip.</param>
        /// <param name="rate">Updated rating and comment data.</param>
        public async Task<RateDTO> UpdateRate(int id,int tripId, UpdateRateDTO rate)
        {
            var existRate = await _rateService.Rates.Where(x=> x.ID == id && x.TripID == tripId).FirstOrDefaultAsync();
            if (existRate != null)
            {

                existRate.Rating = rate.Rating;
                existRate.Comments = rate.Comments;
                _rateService.Entry(existRate).State = EntityState.Modified;
                await _rateService.SaveChangesAsync();

                var updatedRate = await GetRateById(id);

                return updatedRate;
            }
            return null;
        }
    }
}
