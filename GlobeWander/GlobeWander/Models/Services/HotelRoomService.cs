using GlobeWander.Data;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobeWander.Models.Services
{

    public class HotelRoomService : IHotelRoom
    {


        private readonly GlobeWanderDbContext _context;

        public HotelRoomService(GlobeWanderDbContext context)
        {
            _context = context;

        }
        public async Task<HotelRoom> CreateHotelRoom(HotelRoom hotelRoom)
        {

            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task<HotelRoom> GetHotelRoomId(int roomNumber)
        {

            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(roomNumber);

            return hotelRoom;


        }
        public async Task<HotelRoom> DeleteHotelRoom(int roomNumber)
        {

            HotelRoom hotel = await GetHotelRoomId(roomNumber);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return hotel;
        }


        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            var HotelRoom = await _context.HotelRooms.ToListAsync();

            return HotelRoom;
        }

        public async Task<HotelRoom> UpdateHotelRoom(int roomNumber, HotelRoom updatedHotelRoom)
        {
            HotelRoom hotelRoom = await GetHotelRoomId(roomNumber);

            hotelRoom.Price = updatedHotelRoom.Price;
            hotelRoom.IsAvailable = updatedHotelRoom.IsAvailable;


            return hotelRoom;
        }
    }
}
