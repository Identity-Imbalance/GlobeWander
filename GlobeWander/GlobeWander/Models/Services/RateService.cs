using GlobeWander.Data;
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
        public async Task<Rate> Create(Rate rate)
        {
            _rateService.Entry(rate).State = EntityState.Added;
            await _rateService.SaveChangesAsync();
            return rate;

        }

        public async Task<Rate> DeleteRate(int id)
        {
            Rate rate = await GetRateById(id);
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
