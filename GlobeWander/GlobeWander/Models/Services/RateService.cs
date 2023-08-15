using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
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

        public async Task<RateDTO> DeleteRate(int id)
        {
            var rate = await _rateService.Rates.FindAsync(id);

            _rateService.Entry(rate).State = EntityState.Deleted;
            await _rateService.SaveChangesAsync();
            return rate;
        }

        public async Task<List<Rate>> GetAllRate()
        {
            var rates = await _rateService.Rates.ToListAsync();
            return rates;
        }

        public async Task<Rate> GetRateById(int id)
        {
           Rate rate = await _rateService.Rates.FindAsync(id);
            return rate;
        }

        public async Task<Rate> UpdateRate(int id, Rate rate)
        {
            var existRate = await _rateService.Rates.FindAsync(id);
            existRate.Rating = rate.Rating;
            existRate.Comments = rate.Comments;
            await _rateService.SaveChangesAsync();
            return existRate;
        }
    }
}
