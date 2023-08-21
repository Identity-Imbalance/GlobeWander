using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{
    public class RateService : IRate
    {
        private readonly GlobeWanderDbContext _rateService;
        public RateService(GlobeWanderDbContext rateService)
        {
            _rateService = rateService;
        }

        //create not
        public async Task<RateDTO> Create(RateDTO rateDTO)
        {
            var rate = new Rate
            {
                TripID = rateDTO.TripID,
                Comments = rateDTO.Comments,
                Rating = rateDTO.Rating,
            };
            _rateService.Rates.Add(rate);
            await _rateService.SaveChangesAsync();
            return rateDTO;

        }

        public async Task<Rate> DeleteRate(int id ,int TripId)
        {
            var rate = await _rateService.Rates.Where(x => x.ID == id && x.TripID ==TripId).FirstOrDefaultAsync();
           // var rateDto = await GetRateById(id);
            _rateService.Rates.Remove(rate);
            await _rateService.SaveChangesAsync();
            return rate;
           
        }
        public async Task<List<RateDTO>> GetAllRate()
        {
            return await _rateService.Rates.Select(r => new RateDTO
            {
                ID = r.ID,
                TripID = r.TripID,
                Comments = r.Comments,
                Rating = r.Rating
            }
            ).ToListAsync();         
        }

        public async Task<RateDTO> GetRateById(int id, int TripID)
        {
            return await _rateService.Rates.Where(x => x.ID == id && x.TripID == TripID).Select(r => new RateDTO
            {
                ID = r.ID,
                TripID = r.TripID,
                Comments = r.Comments,
                Rating = r.Rating
            }
             ).FirstOrDefaultAsync();
        }

        //       public async Task<RateDTO> UpdateRate(int id, int tripId, RateDTO rateDTO)
        //{
        //    var existRate = await _rateService.Rates.FindAsync(id, tripId);
        //    if (existRate != null)
        //    {
        //        existRate.Rating = rateDTO.Rating;
        //        existRate.Comments = rateDTO.Comments;
        //        await _rateService.SaveChangesAsync();
        //        return rateDTO;
        //    }
        //    return null;
        //}

        public async Task<RateDTO> UpdateRate(int id, int tripId, RateDTO rateDTO)
        {
            var existRate = await _rateService.Rates
                .Where(r => r.ID == id && r.TripID == tripId)
                .FirstOrDefaultAsync();

            if (existRate != null)
            {
                existRate.Rating = rateDTO.Rating;
                existRate.Comments = rateDTO.Comments;
                await _rateService.SaveChangesAsync();
                return rateDTO;
            }
            return null;
        }



    }
}
