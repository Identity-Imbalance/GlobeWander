using GlobeWander.Data;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{
    public class HotelService : IHotel
    {


        private readonly GlobeWanderDbContext _context;

        public HotelService(GlobeWanderDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);

            await _context.SaveChangesAsync();


            return hotel;
        }

        public async Task<Hotel> DeleteHotel(int id)

        {
            Hotel hotel = await GetHotelId(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return hotel;
        }

        public async Task<Hotel> GetHotelId(int hotelId)
        {
            Hotel hotel = await _context.Hotels.FindAsync(hotelId);

            return hotel;

        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();

            return hotels;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel updateedHotel)
        {
            Hotel hotel = await GetHotelId(id);
            hotel.Name = updateedHotel.Name;
            hotel.Description = updateedHotel.Description;


            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return hotel;
        }
    }
}

