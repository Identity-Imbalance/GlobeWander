using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<hotelroomDTOcreate> CreateHotelRoom(hotelroomDTOcreate hotelRoomdto)
        {
            HotelRoom hotelRooms = new HotelRoom()
            {RoomNumber= hotelRoomdto.HotelID*100 + hotelRoomdto.RoomID,
                HotelID= hotelRoomdto.HotelID,
                RoomID= hotelRoomdto.RoomID,
                Price= hotelRoomdto.Price,
                IsAvailable=true,

            };
         
            hotelRoomdto.RoomNumber = hotelRooms.RoomNumber;

            _context.HotelRooms.Add(hotelRooms);
            await _context.SaveChangesAsync();
            return hotelRoomdto;
        }
        public async Task<HotelRoomDTO> GetHotelRoomId(int hotelID,int roomNumber)
        {

            var hotelRoomDTO = await _context.HotelRooms.Select(b => new HotelRoomDTO
            {
                HotelID = b.HotelID,
                RoomNumber = b.RoomNumber,
                RoomID = b.RoomID,
                Price = b.Price,
                IsAvailable = b.IsAvailable,
                Rooms = _context.Rooms.Select(x => new RoomDTO { ID = x.ID, Name = x.Name, Layout = x.Layout }
            ).Where(x => x.ID == b.RoomID).FirstOrDefault()
            }).Where(x=>x.HotelID== hotelID && x.RoomNumber== roomNumber).FirstOrDefaultAsync();
            return hotelRoomDTO;


        }
        public async Task<HotelRoomDTO> DeleteHotelRoom(int hotelID, int roomNumber)
        {

            HotelRoomDTO hotel = await GetHotelRoomId(hotelID, roomNumber);
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelID, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return hotel;
        }


        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {

            var hotelRoomDTO = await _context.HotelRooms.Select(b => new HotelRoomDTO {
                HotelID = b.HotelID,
                RoomNumber = b.RoomNumber,
                RoomID = b.RoomID,
                Price = b.Price,
                IsAvailable = b.IsAvailable,
                Rooms =  _context.Rooms.Select(x => new RoomDTO { ID = x.ID, Name = x.Name, Layout = x.Layout }
             ).Where(x => x.ID == b.RoomID).FirstOrDefault()
            }).ToListAsync();

          
            return hotelRoomDTO;
        }

        public async Task<hotelroomDTOcreate> UpdateHotelRoom(int hotelId, int roomNumber, hotelroomDTOcreate updatedHotelRoom)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            
                hotelRoom.Price = updatedHotelRoom.Price;
                hotelRoom.IsAvailable = updatedHotelRoom.IsAvailable;
                _context.Entry(hotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            updatedHotelRoom.HotelID = hotelId;
            updatedHotelRoom.RoomNumber = roomNumber;
            updatedHotelRoom.RoomID = hotelRoom.RoomID;
                return updatedHotelRoom;
          

            
        
        }
    }
}
