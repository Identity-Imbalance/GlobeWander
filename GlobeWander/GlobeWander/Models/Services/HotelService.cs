using GlobeWander.Data;
using GlobeWander.Models.DTO;
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

        public async Task<HotelDTO> CreateHotel(HotelDTO hotelDTO)
        {
            Hotel hotel =new Hotel() {    Name= hotelDTO.Name,
                Description= hotelDTO.Description,
                TourSpotID= hotelDTO.TourSpotID
            };
            hotel.TourSpot = await _context.TourSpots.FindAsync(hotel.TourSpotID);
            _context.Hotels.Add(hotel);
            hotel.Id = hotelDTO.Id;
            await _context.SaveChangesAsync();


            return hotelDTO;
        }

        public async Task<Hotel> DeleteHotel(int id)

        {
           // Hotel hoteldto = await GetHotelId(id);
            var hotel= await _context.Hotels.FindAsync(id);
             
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return hotel;
        }

        public async Task<HotelDTO> GetHotelId(int hotelId)
        {
            var hotelrooms = await _context.HotelRooms.Where(x => x.HotelID == hotelId).Select(v => new HotelRoomDTO
            {
                HotelID = v.HotelID,
                RoomID = v.RoomID,
                IsAvailable = v.IsAvailable,
                RoomNumber = v.RoomNumber,
                PricePerDay = v.PricePerDay

            }).ToListAsync();

            HotelDTO hotel = await _context.Hotels.Where(x => x.Id == hotelId).Select(b => new HotelDTO
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                TourSpotID = b.TourSpotID,
                HotelRoom = hotelrooms
            }).FirstOrDefaultAsync();
            ;
            return hotel;

        }

        public async Task<List<HotelDTO>> GetAllHotels()
        {
            

            List<HotelDTO> hotel = await _context.Hotels.Select(b => new HotelDTO
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                TourSpotID = b.TourSpotID,
                HotelRoom = _context.HotelRooms.Where(x=>x.HotelID==b.Id).Select(v => new HotelRoomDTO
                {
                    HotelID = v.HotelID,
                    RoomID = v.RoomID,
                    IsAvailable = v.IsAvailable,
                    RoomNumber = v.RoomNumber,
                    PricePerDay = v.PricePerDay

                }).ToList()
        }).ToListAsync();
          

            return hotel;
        }

        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO updatedHotel)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            

            hotel.Name = updatedHotel.Name;
            hotel.Description = updatedHotel.Description;


            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            updatedHotel.Id = id;
            updatedHotel.TourSpotID= hotel.TourSpotID;

            return updatedHotel;
        }
    }
}

